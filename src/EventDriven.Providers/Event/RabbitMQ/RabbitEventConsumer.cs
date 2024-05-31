using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using UpscaleDown.EventDriven.Architecture.Configuration;
using UpscaleDown.EventDriven.Repository.Builders;
using UpscaleDown.EventDriven.Repository.Interfaces.Entities;
using UpscaleDown.EventDriven.Events;
using UpscaleDown.EventDriven.Events.Constants;

namespace UpscaleDown.EventDriven.Providers.Event.RabbitMQ;

public class RabbitMQEventConsumer<T> : IEventConsumer<T> where T : IRecord
{
    #region Private members
    private readonly string _resource;

    private readonly RabbitMQOptions _options;

    private readonly ConnectionFactory _factory;

    private readonly ILogger<RabbitMQEventConsumer<T>> _logger;

    private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions()
    {
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
        PropertyNameCaseInsensitive = true,
        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };
    #endregion
    #region Constructors
    public RabbitMQEventConsumer(RabbitMQOptions rbOptions, EventDrivenOptions options, ILogger<RabbitMQEventConsumer<T>> logger)
    {
        _resource = ResourceBuilder
        .Provider(options.Provider)
        .Origin(options.Origin)
        .Entity<T>()
        .Build();

        _options = rbOptions;
        _logger = logger;
        _factory = new ConnectionFactory { HostName = _options.HOST };
    }
    #endregion
    #region Interface methods
    public Task OnAddedAsync(T added)
    {
        throw new NotImplementedException();
    }

    public Task OnAddedManyAsync(IEnumerable<T> added)
    {
        throw new NotImplementedException();
    }

    public Task OnOtherAsync(string type, IEnumerable<T> other)
    {
        throw new NotImplementedException();
    }

    public Task OnRemovedAsync(T removed)
    {
        throw new NotImplementedException();
    }

    public Task OnRemovedManyAsync(IEnumerable<T> removed)
    {
        throw new NotImplementedException();
    }

    public Task OnUpdatedAsync(T updated)
    {
        throw new NotImplementedException();
    }

    public Task OnUpdatedManyAsync(IEnumerable<T> updated)
    {
        throw new NotImplementedException();
    }

    public async Task ListenAsync(CancellationToken cancellationToken)
    {

        var t = Task.Run(() =>
        {
            UseTempChannel((channel) =>
            {
                var consumer = new MessageReceiver<T>(channel);
                consumer.OnReceive += (content) =>
                {
                    try
                    {
                        switch (content.Type)
                        {
                            case EventTypes.Added: OnAddedAsync((T)content.Data!); break;
                            case EventTypes.Updated: OnUpdatedAsync((T)content.Data!); break;
                            case EventTypes.Removed: OnRemovedAsync((T)content.Data!); break;
                            case EventTypes.AddedMany: OnAddedManyAsync((List<T>)content.Data!); break;
                            case EventTypes.UpdatedMany: OnUpdatedManyAsync((List<T>)content.Data!); break;
                            case EventTypes.RemovedMany: OnRemovedManyAsync((List<T>)content.Data!); break;
                            default:
                                if (typeof(List<T>).IsAssignableFrom(content.Data!.GetType()))
                                {
                                    OnOtherAsync(content.Type, (List<T>)content.Data!);
                                    break;
                                }
                                var l = new List<T>
                            {
                                (T)content.Data!
                            };
                                OnOtherAsync(content.Type, l);
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message, ex);
                    }
                };

                // consumer.OnJson += (json) =>
                // {
                //     OnJson?.Invoke(json);
                // };

                channel.BasicConsume(queue: channel.CurrentQueue,
                                     autoAck: false,
                                     consumer: consumer);

                cancellationToken.WaitHandle.WaitOne();
            });
        });

        await t;
    }
    #endregion

    #region Private methods
    private void UseTempChannel(Action<IModel> func)
    {
        using var connection = _factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.ExchangeDeclare(exchange: "eventdriven.broadcast", type: ExchangeType.Topic);

        var queueName = channel.QueueDeclare(
                     durable: true,
                     exclusive: true,
                     autoDelete: true,
                     arguments: null).QueueName;

        channel.QueueBind(queueName, "eventdriven.broadcast", _resource);

        func(channel);
    }
    #endregion
}

internal class MessageReceiver<T> : DefaultBasicConsumer
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions()
    {
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
        PropertyNameCaseInsensitive = true,
        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    private readonly IModel _channel;
    public event Action<Events.Event> OnReceive;

    public event Action<string> OnJson;
    public MessageReceiver(IModel channel)
    {
        _channel = channel;
    }
    public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, ReadOnlyMemory<byte> body)
    {
        var message = Encoding.UTF8.GetString(body.ToArray());
        var content = JsonSerializer.Deserialize<Events.Event>(message, _jsonSerializerOptions);
        OnJson?.Invoke(message);
        OnReceive?.Invoke(content!);
        _channel.BasicAck(deliveryTag, false);
    }
}

using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using UpscaleDown.EventDriven.Architecture.Configuration;
using UpscaleDown.EventDriven.Repository.Builders;
using UpscaleDown.EventDriven.Repository.Interfaces.Entities;
using UpscaleDown.EventDriven.Events.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace UpscaleDown.EventDriven.Providers.Event.RabbitMQ;

public class RabbitEventConsumer<T> : IEventConsumer<T> where T : IRecord
{
    #region Private members
    private readonly string _resource;

    private readonly RabbitMQOptions _options;

    private readonly ConnectionFactory _factory;

    private readonly ILogger<RabbitEventConsumer<T>> _logger;

    private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions()
    {
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
        PropertyNameCaseInsensitive = true,
        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    private readonly IServiceProvider _serviceProvider;
    #endregion
    #region Constructors
    public RabbitEventConsumer(Core.EventDriven ev, RabbitMQOptions rbOptions, EventDrivenOptions options, ILogger<RabbitEventConsumer<T>> logger)
    {
        _resource = ResourceBuilder
        .Provider("*")
        .Origin("*")
        .Entity<T>()
        .BuildTopic();

        _options = rbOptions;
        _logger = logger;
        _factory = new ConnectionFactory { HostName = _options.Host };
        _serviceProvider = ev.GetServiceProvider();
    }
    #endregion
    #region Interface methods

    public async Task ListenAsync(CancellationToken cancellationToken)
    {

        var t = Task.Run(() =>
        {
            UseTempChannel((channel) =>
            {
                var consumer = new MessageReceiver<T>(channel);
                consumer.OnReceive += async (content) =>
                {
                    await PublishToHandlersAsync(content);
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

    private async Task PublishToHandlersAsync(Events.Event @event)
    {
        var svcs = _serviceProvider.GetKeyedServices<IEventHandler<T>>(@event.Type);

        if (svcs == null || svcs.Count() == 0) return;

        foreach (var svc in svcs)
        {
            try
            {
                if (typeof(List<T>).IsAssignableFrom(@event.Data!.GetType()))
                {
                    await svc.HandleAsync((List<T>)@event.Data!, @event);
                    continue;
                }
                await svc.HandleAsync((T)@event.Data!, @event);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message, ex);
            }


        }
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

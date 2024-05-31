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

public class RabbitEventPublisher<T> : IEventPublisher<T> where T : IRecord
{

    #region Private members
    private readonly string _resource;

    private readonly RabbitMQOptions _options;

    private readonly ConnectionFactory _factory;

    private readonly ILogger<RabbitEventPublisher<T>> _logger;

    private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions()
    {
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
        PropertyNameCaseInsensitive = true,
        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };
    #endregion

    public RabbitEventPublisher(RabbitMQOptions rbOptions, EventDrivenOptions options, ILogger<RabbitEventPublisher<T>> logger)
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

    #region Interface implementation
    public async Task PublishAddedAsync(T added)
    {
        var @event = new Events.Event()
        {
            Guid = Guid.NewGuid().ToString(),
            Data = added,
            Resource = _resource,
            Type = EventTypes.Added,
        };

        await Publish(@event);
    }

    public async Task PublishAddedManyAsync(IEnumerable<T> added)
    {
        var @event = new Events.Event()
        {
            Guid = Guid.NewGuid().ToString(),
            Data = added,
            Resource = _resource,
            Type = EventTypes.AddedMany,
        };

        await Publish(@event);
    }

    public async Task PublishOtherAsync(string type, IEnumerable<T> other)
    {
        var @event = new Events.Event()
        {
            Guid = Guid.NewGuid().ToString(),
            Data = other,
            Resource = _resource,
            Type = type,
        };

        await Publish(@event);
    }

    public async Task PublishRemovedAsync(T removed)
    {
        var @event = new Events.Event()
        {
            Guid = Guid.NewGuid().ToString(),
            Data = removed,
            Resource = _resource,
            Type = EventTypes.Removed,
        };

        await Publish(@event);
    }

    public async Task PublishRemovedManyAsync(IEnumerable<T> removed)
    {
        var @event = new Events.Event()
        {
            Guid = Guid.NewGuid().ToString(),
            Data = removed,
            Resource = _resource,
            Type = EventTypes.RemovedMany,
        };

        await Publish(@event);
    }

    public async Task PublishUpdatedAsync(T updated)
    {
        var @event = new Events.Event()
        {
            Guid = Guid.NewGuid().ToString(),
            Data = updated,
            Resource = _resource,
            Type = EventTypes.Updated,
        };

        await Publish(@event);
    }

    public async Task PublishUpdatedManyAsync(IEnumerable<T> updated)
    {
        var @event = new Events.Event()
        {
            Guid = Guid.NewGuid().ToString(),
            Data = updated,
            Resource = _resource,
            Type = EventTypes.UpdatedMany,
        };

        await Publish(@event);
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

    private async Task Publish(Events.Event @event)
    {
        var counter = 0;
        A:
        try
        {
            UseTempChannel((channel) =>
            {
                var json = JsonSerializer.Serialize(@event, _jsonSerializerOptions);
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish("eventdriven.broadcast", _resource, channel.CreateBasicProperties(), body);
            });
        }
        catch (Exception ex)
        {
            if (_options.RetryCount > 0)
            {
                if (counter < _options.RetryCount)
                {
                    counter++;
                    await Task.Delay(_options.RetryDelay);
                    goto A;
                }
            }

            _logger.LogError(ex.Message);
        }

    }
    #endregion
}
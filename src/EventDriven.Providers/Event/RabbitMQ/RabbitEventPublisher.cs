using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using RabbitMQ.Client;
using UpscaleDown.EventDriven.Architecture.Configuration;
using UpscaleDown.EventDriven.Core.Builders;
using UpscaleDown.EventDriven.Core.Interfaces.Entities;
using UpscaleDown.EventDriven.Events;
using UpscaleDown.EventDriven.Events.Constants;

namespace UpscaleDown.EventDriven.Providers.Event.RabbitMQ;

public class RabbitEventPublisher<T> : IEventPublisher<T> where T : IRecord
{

    #region Private members
    private readonly string _resource;

    private readonly RabbitMQOptions _options;

    private readonly ConnectionFactory _factory;

    private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions()
    {
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
        PropertyNameCaseInsensitive = true,
        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };
    #endregion

    public RabbitEventPublisher(RabbitMQOptions rbOptions, EventDrivenOptions options)
    {
        _resource = ResourceBuilder
        .Provider(options.PROVIDER)
        .Origin(options.ORIGIN)
        .Entity<T>()
        .Build();

        _options = rbOptions;
    }

    #region Interface implementation
    public async Task PublishAddedAsync(T added)
    {
        var @event = new Event<T>()
        {
            Guid = Guid.NewGuid().ToString(),
            Data = added,
            Resource = _resource,
            Type = EventTypes.Added,
        };

        Publish(@event);
    }

    public async Task PublishAddedManyAsync(IEnumerable<T> added)
    {
        var @event = new Event<T>()
        {
            Guid = Guid.NewGuid().ToString(),
            Data = added,
            Resource = _resource,
            Type = EventTypes.AddedMany,
        };

        Publish(@event);
    }

    public async Task PublishOtherAsync(string type, IEnumerable<T> other)
    {
        var @event = new Event<T>()
        {
            Guid = Guid.NewGuid().ToString(),
            Data = other,
            Resource = _resource,
            Type = type,
        };

        Publish(@event);
    }

    public async Task PublishRemovedAsync(T removed)
    {
        var @event = new Event<T>()
        {
            Guid = Guid.NewGuid().ToString(),
            Data = removed,
            Resource = _resource,
            Type = EventTypes.Removed,
        };

        Publish(@event);
    }

    public async Task PublishRemovedManyAsync(IEnumerable<T> removed)
    {
        var @event = new Event<T>()
        {
            Guid = Guid.NewGuid().ToString(),
            Data = removed,
            Resource = _resource,
            Type = EventTypes.RemovedMany,
        };

        Publish(@event);
    }

    public async Task PublishUpdatedAsync(T updated)
    {
        var @event = new Event<T>()
        {
            Guid = Guid.NewGuid().ToString(),
            Data = updated,
            Resource = _resource,
            Type = EventTypes.Updated,
        };

        Publish(@event);
    }

    public async Task PublishUpdatedManyAsync(IEnumerable<T> updated)
    {
        var @event = new Event<T>()
        {
            Guid = Guid.NewGuid().ToString(),
            Data = updated,
            Resource = _resource,
            Type = EventTypes.UpdatedMany,
        };

        Publish(@event);
    }
    #endregion

    #region Private methods
    private void UseTempChannelAsync(Action<IModel> func)
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

    private void Publish(Event<T> @event)
    {
        UseTempChannelAsync((channel) =>
        {
            var json = JsonSerializer.Serialize(@event, _jsonSerializerOptions);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish("eventdriven.broadcast", _resource, channel.CreateBasicProperties(), body);
        });
    }
    #endregion
}
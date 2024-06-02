using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using UpscaleDown.EventDriven.Repository.Interfaces.Entities;
using UpscaleDown.EventDriven.Events;
using UpscaleDown.EventDriven.Providers.Data.Mongo;
using UpscaleDown.EventDriven.Providers.Data.Mongo.Configuration;
using UpscaleDown.EventDriven.Providers.Event.RabbitMQ;
using UpscaleDown.EventDriven.Architecture.Extensions;
using UpscaleDown.EventDriven.Core;
using UpscaleDown.EventDriven.Events.Interfaces;

namespace UpscaleDown.EventDriven.Providers.Extensions;

public static class EventDrivenExtensions
{
    private static bool _rabbitSetup = false;
    private static bool _mongoSetup = false;

    #region Mongo
    public static Core.EventDriven SetupWithMongoDb(this Core.EventDriven ev, Action<MongoDbOptions> opts)
    {
        var services = ev.Services;
        var options = new MongoDbOptions();
        opts(options);
        services.AddSingleton(options);
        var client = new MongoClient(options.MONGODB_URI);
        services.AddSingleton(client);
        _mongoSetup = true;
        return ev;
    }
    public static Core.EventDriven AddMongoRecordService<T>(this Core.EventDriven ev)
    where T : IRecord
    {
        var services = ev.Services;
        if (!_mongoSetup) throw new MongoConfigurationException("You must setup mongo before adding services");
        services.AddRecordServices<T, MongoRecordRepository<T>>();
        return ev;
    }

    public static Core.EventDriven AddMongoNodeService<T>(this Core.EventDriven ev)
    where T : INodeRecord
    {
        var services = ev.Services;
        if (!_mongoSetup) throw new MongoConfigurationException("You must setup mongo before adding services");
        services.AddNodeRecordServices<T, MongoNodeRepository<T>>();
        return ev;
    }
    #endregion
    #region Rabbit
    public static Core.EventDriven SetupWithRabbitMQ(this Core.EventDriven ev, Action<RabbitMQOptions> opts)
    {
        var services = ev.Services;
        var options = new RabbitMQOptions();
        opts(options);
        services.AddSingleton(options);
        _rabbitSetup = true;
        return ev;
    }

    public static Core.EventDriven AddRabbitEventPublisher<T>(this Core.EventDriven ev)
    where T : IRecord
    {
        var services = ev.Services;
        if (!_rabbitSetup) throw new Exception("You must setup rabbitmq before adding publishers");
        services.AddScoped<IEventPublisher<T>, RabbitEventPublisher<T>>();
        return ev;
    }

    public static Core.EventDriven AddRabbitEventConsumer<T>(this Core.EventDriven ev)
    where T : IRecord
    {
        var services = ev.Services;
        if (!_rabbitSetup) throw new Exception("You must setup rabbitmq before adding consumers");
        services.AddSingleton<IBaseEventConsumer, RabbitEventConsumer<T>>();
        return ev;
    }
    #endregion
}
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using UpscaleDown.EventDriven.Core.Interfaces.Entities;
using UpscaleDown.EventDriven.Events;
using UpscaleDown.EventDriven.Providers.Data.Mongo;
using UpscaleDown.EventDriven.Providers.Data.Mongo.Configuration;
using UpscaleDown.EventDriven.Providers.Event.RabbitMQ;
using UpscaleDown.EventDriven.Architecture.Extensions;

namespace UpscaleDown.EventDriven.Providers.Extensions;

public static class IServiceCollectionExtensions
{
    private static bool _rabbitSetup = false;
    private static bool _mongoSetup = false;
    public static IServiceCollection SetupWithMongoDb(this IServiceCollection services, Action<MongoDbOptions> opts)
    {
        var options = new MongoDbOptions();
        opts(options);
        services.AddSingleton(options);
        var client = new MongoClient(options.MONGODB_URI);
        services.AddSingleton(client);
        _mongoSetup = true;
        return services;
    }
    public static IServiceCollection AddMongoRecordService<T>(this IServiceCollection services)
    where T : IRecord
    {
        if (_mongoSetup) throw new MongoConfigurationException("You must setup mongo before adding services");
        services.AddRecordServices<T, MongoRecordRepository<T>>();
        return services;
    }

    public static IServiceCollection AddMongoNodeService<T>(this IServiceCollection services)
    where T : INodeRecord
    {
        if (_mongoSetup) throw new MongoConfigurationException("You must setup mongo before adding services");
        services.AddNodeRecordServices<T, MongoNodeRepository<T>>();
        return services;
    }

    public static IServiceCollection SetupWithRabbitMQ(this IServiceCollection services, Action<RabbitMQOptions> opts)
    {
        var options = new RabbitMQOptions();
        opts(options);
        services.AddSingleton(options);
        _rabbitSetup = true;
        return services;
    }

    public static IServiceCollection AddRabbitEventPublisher<T>(this IServiceCollection services)
    where T : IRecord
    {
        if (_rabbitSetup) throw new Exception("You must setup rabbitmq before adding publishers");
        services.AddScoped<IEventPublisher<T>, RabbitEventPublisher<T>>();
        return services;
    }

}
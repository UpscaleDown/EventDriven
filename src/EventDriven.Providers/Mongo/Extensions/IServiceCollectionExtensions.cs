using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using UpscaleDown.EventDriven.Core.Interfaces.Entities;
using UpscaleDown.EventDriven.Providers.Mongo.Configuration;
using UpscaleDown.EventDriven.Repository.Interfaces;

namespace UpscaleDown.EventDriven.Architecture.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection SetupWithMongoDb(this IServiceCollection services, Action<MongoDbOptions> opts)
    {
        var options = new MongoDbOptions();
        opts(options);
        services.AddSingleton(options);
        var client = new MongoClient(options.MONGODB_URI);
        services.AddSingleton(client);
        return services;
    }
}
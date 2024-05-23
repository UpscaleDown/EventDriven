using Microsoft.Extensions.DependencyInjection;
using UpscaleDown.EventDriven.Architecture.Configuration;
using UpscaleDown.EventDriven.Architecture.Interfaces.Services;
using UpscaleDown.EventDriven.Architecture.Services;
using UpscaleDown.EventDriven.Core.Interfaces.Entities;
using UpscaleDown.EventDriven.Repository;
using UpscaleDown.EventDriven.Repository.Interfaces;

namespace UpscaleDown.EventDriven.Architecture.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddRecordServices<TRecord, TRepository>(this IServiceCollection services) 
    where TRecord : IRecord 
    where TRepository : class, IRecordRepository<TRecord>
    {
        services.AddScoped<IRecordRepository<TRecord>, TRepository>();
        services.AddScoped<IRecordService<TRecord>, RecordService<TRecord>>();
        return services;
    }

    public static IServiceCollection AddNodeRecordServices<TNodeRecord, TRepository>(this IServiceCollection services) 
    where TNodeRecord : INodeRecord
    where TRepository : class, INodeRepository<TNodeRecord>
    {
        services.AddScoped<INodeRepository<TNodeRecord>, TRepository>();
        services.AddScoped<INodeService<TNodeRecord>, NodeService<TNodeRecord>>();
        return services;
    }

    public static IServiceCollection SetupEventDriven(this IServiceCollection services, Action<EventDrivenOptions> opts)
    {
        var options = new EventDrivenOptions();
        opts(options);
        services.AddSingleton(options);

        // #TODO: Add Auth setup here in case AuthOptions are used
        return services;
    }
}
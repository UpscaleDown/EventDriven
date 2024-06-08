using Microsoft.Extensions.DependencyInjection;
using UpscaleDown.EventDriven.Architecture.Interfaces.Services;
using UpscaleDown.EventDriven.Architecture.Services;
using UpscaleDown.EventDriven.Repository.Interfaces;
using UpscaleDown.EventDriven.Repository.Interfaces.Entities;

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
        services.AddScoped<IRecordRepository<TNodeRecord>, TRepository>();
        services.AddScoped<INodeRepository<TNodeRecord>, TRepository>();
        services.AddScoped<INodeService<TNodeRecord>, NodeService<TNodeRecord>>();
        return services;
    }
}
using Microsoft.Extensions.DependencyInjection;
using UpscaleDown.EventDriven.Architecture.Interfaces.Services;
using UpscaleDown.EventDriven.Architecture.Services;
using UpscaleDown.EventDriven.Core.Interfaces.Entities;
using UpscaleDown.EventDriven.Repository;
using UpscaleDown.EventDriven.Repository.Interfaces;

namespace UpscaleDown.EventDriven.Architecture.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddRecordServices<TRecord>(this IServiceCollection services) where TRecord : IRecord
    {
        services.AddScoped<IRecordRepository<TRecord>, RecordRepository<TRecord>>();
        services.AddScoped<IRecordService<TRecord>, RecordService<TRecord>>();
        return services;
    }

    public static IServiceCollection AddNodeRecortServices<TNodeRecord>(this IServiceCollection services) where TNodeRecord : INodeRecord
    {
        services.AddScoped<INodeRepository<TNodeRecord>, NodeRepository<TNodeRecord>>();
        services.AddScoped<INodeService<TNodeRecord>, NodeService<TNodeRecord>>();
        return services;
    }
}
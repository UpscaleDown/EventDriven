using UpscaleDown.EventDriven.Repository.Interfaces.Entities;
using UpscaleDown.EventDriven.Exceptions;

namespace UpscaleDown.EventDriven.Repository.Builders;

public class ResourceBuilder
{
    private string? _provider;
    private string? _origin;
    private string? _entity;

    public static ResourceBuilder Provider(string provider)
    {
        return new ResourceBuilder
        {
            _provider = provider.ToLowerInvariant()
        };
    }

    public ResourceBuilder Origin(string origin)
    {
        _origin = origin.ToLowerInvariant();
        return this;
    }

    public ResourceBuilder Entity(string entity)
    {
        _entity = entity.ToLowerInvariant();
        return this;
    }

    public ResourceBuilder Entity<T>() where T : IRecord
    {
        _entity = typeof(T).Name.ToLowerInvariant();
        return this;
    }

    public string Build()
    {
        if (string.IsNullOrWhiteSpace(_provider) || string.IsNullOrWhiteSpace(_origin) || string.IsNullOrWhiteSpace(_entity))
            throw new InvalidResourceException(Errors.InvalidResourceBuilderCode, Errors.InvalidResourceBuilderMessage);
        return $"{_provider}::{_origin}::{_entity}";
    }

    public static string Build(Action<ResourceBuilderOptions> options)
    {
        var opts = new ResourceBuilderOptions();
        options(opts);
        var builder = ResourceBuilder.Provider(opts.Provider).Origin(opts.Origin).Entity(opts.Entity);
        return builder.Build();
    }
}
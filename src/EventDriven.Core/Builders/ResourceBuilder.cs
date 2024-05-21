using UpscaleDown.EventDriven.Core.Interfaces.Entities;
using UpscaleDown.EventDriven.Exceptions;

namespace UpscaleDown.EventDriven.Core.Builders;

public class ResourceBuilder
{
    private string _provider;
    private string _origin;
    private string _entity;

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
        if (_provider == null || _origin == null || _entity == null)
            throw new InvalidResourceException(Errors.InvalidResourceBuilderCode, Errors.InvalidResourceBuilderMessage);
        return $"{_provider}::{_origin}::{_entity}";
    }
}
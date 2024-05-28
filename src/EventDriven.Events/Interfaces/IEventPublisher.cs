using UpscaleDown.EventDriven.Core.Interfaces.Entities;

namespace UpscaleDown.EventDriven.Events;

public interface IEventPublisher<T> where T : IRecord
{
    public Task PublishAddedAsync(T added);
    
    public Task PublishUpdatedAsync(T updated);

    public Task PublishRemovedAsync(T removed);

    public Task PublishAddedManyAsync(IEnumerable<T> added);

    public Task PublishUpdatedManyAsync(IEnumerable<T> updated);

    public Task PublishRemovedManyAsync(IEnumerable<T> removed);

    public Task PublishOtherAsync(string type, IEnumerable<T> other);
}
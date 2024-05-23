using UpscaleDown.EventDriven.Core.Interfaces.Entities;

namespace UpscaleDown.EventDriven.Events;

public interface IEventConsumer<T> where T : IRecord
{
    public Task OnAddedAsync(T added);
    
    public Task OnUpdatedAsync(T updated);

    public Task OnRemovedAsync(T removed);

    public Task OnAddedManyAsync(IEnumerable<T> added);

    public Task OnUpdatedManyAsync(IEnumerable<T> updated);

    public Task OnRemovedManyAsync(IEnumerable<T> removed);

    public Task OnOtherAsync(IEnumerable<T> other);
}
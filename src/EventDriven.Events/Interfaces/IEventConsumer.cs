using UpscaleDown.EventDriven.Repository.Interfaces.Entities;

namespace UpscaleDown.EventDriven.Events;

public interface IEventConsumer<T> : IBaseEventConsumer where T : IRecord
{
    public Task OnAddedAsync(T added);

    public Task OnUpdatedAsync(T updated);

    public Task OnRemovedAsync(T removed);

    public Task OnAddedManyAsync(IEnumerable<T> added);

    public Task OnUpdatedManyAsync(IEnumerable<T> updated);

    public Task OnRemovedManyAsync(IEnumerable<T> removed);

    public Task OnOtherAsync(string type, IEnumerable<T> other);

    public Task ListenAsync(CancellationToken cancellationToken);
}
using UpscaleDown.EventDriven.Repository.Interfaces.Entities;

namespace UpscaleDown.EventDriven.Events.Interfaces;

public interface IEventConsumer<T> : IBaseEventConsumer where T : IRecord
{
    public Task ListenAsync(CancellationToken cancellationToken);
}
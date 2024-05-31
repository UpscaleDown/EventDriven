using UpscaleDown.EventDriven.Repository.Interfaces.Entities;

namespace UpscaleDown.EventDriven.Events;

public interface IBaseEventConsumer
{
    public Task ListenAsync(CancellationToken cancellationToken);
}
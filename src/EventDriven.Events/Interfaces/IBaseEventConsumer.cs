namespace UpscaleDown.EventDriven.Events.Interfaces;

public interface IBaseEventConsumer
{
    public Task ListenAsync(CancellationToken cancellationToken);
}
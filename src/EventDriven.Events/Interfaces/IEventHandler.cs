namespace UpscaleDown.EventDriven.Events.Interfaces;

public interface IEventHandler<T>
{
    public Task HandleAsync(T data, Events.Event @event);
    public Task HandleAsync(List<T> data, Events.Event @event);
}
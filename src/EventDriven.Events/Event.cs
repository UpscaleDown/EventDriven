using UpscaleDown.EventDriven.Repository.Interfaces.Entities;

namespace UpscaleDown.EventDriven.Events;

public class Event: IEvent
{
    public string Guid { get; set; }

    public DateTime Date { get; set; } = DateTime.UtcNow;
    public string Resource { get; set; }

    public string Type { get; set; }

    public object? Data { get; set; }
}
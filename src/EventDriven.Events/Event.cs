using UpscaleDown.EventDriven.Core.Interfaces.Entities;

namespace UpscaleDown.EventDriven.Events;

public class Event<T> : IEvent<T> where T : IRecord
{
    public string Guid { get; set; }

    public DateTime Date { get; set; } = DateTime.UtcNow;
    public string Resource { get; set; }

    public string Type { get; set; }

    public object? Data { get; set; }
}
using UpscaleDown.EventDriven.Core.Interfaces.Entities;

namespace UpscaleDown.EventDriven.Events;

public class Event<T> : IEvent<T> where T : IRecord
{
    public string Resource { get; set; }
    public string Operation { get; set; }
    public T? Data { get; set; }
}
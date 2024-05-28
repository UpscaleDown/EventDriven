using UpscaleDown.EventDriven.Core.Interfaces.Entities;

namespace UpscaleDown.EventDriven.Events;

public interface IEvent<T> where T : IRecord
{
    public string Guid { get; set; }

    public DateTime Date { get; set; }

    public string Resource { get; set; }

    public string Type { get; set; }

    public object Data { get; set; }
}
namespace UpscaleDown.EventDriven.Repository.Interfaces.Entities;

public interface IRecord
{
    /// <summary>
    /// Unique Id of the record
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Resource identifier of the record
    /// </summary>
    public string Resource { get; set; }

    /// <summary>
    /// Indicates if events related to this record should be stored
    /// </summary>
    public bool Store { get; set; }
}
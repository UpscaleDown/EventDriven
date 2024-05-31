namespace UpscaleDown.EventDriven.Repository.Interfaces.Entities;

public interface INodeRecord : IRecord
{
    /// <summary>
    /// Noded name separated by '.'
    /// </summary>
    string Name { get; set; }
}
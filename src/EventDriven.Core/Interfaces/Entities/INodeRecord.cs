namespace UpscaleDown.EventDriven.Core.Interfaces.Entities;

public interface INodeRecord : IRecord
{
    /// <summary>
    /// Noded name separated by '.'
    /// </summary>
    string Name { get; set; }
}
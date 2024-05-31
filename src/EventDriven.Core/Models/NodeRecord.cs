using UpscaleDown.EventDriven.Repository.Interfaces.Entities;

namespace UpscaleDown.EventDriven.Core.Models;

#pragma warning disable CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
public class NodeRecord : Record, INodeRecord
#pragma warning restore CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
{
    #region Interface Properties
    public string Name { get; set; }
    #endregion

    #region Public Properties

    #endregion

    #region Private Properties

    #endregion

    #region Constructors
    public NodeRecord()
    {
        Name = "";
    }

    public NodeRecord(string name)
    {
        Name = name;
    }
    #endregion

    #region Public Methods

    #endregion

    #region  Private Methods

    #endregion
}
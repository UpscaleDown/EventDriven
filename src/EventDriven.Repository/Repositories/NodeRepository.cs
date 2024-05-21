using UpscaleDown.EventDriven.Core.Interfaces.Entities;
using UpscaleDown.EventDriven.Repository.Interfaces;

namespace UpscaleDown.EventDriven.Repository;

public class NodeRepository<TNodeRecord> : RecordRepository<TNodeRecord>, INodeRepository<TNodeRecord> where TNodeRecord : INodeRecord
{
    public TNodeRecord FindByName(string name)
    {
        throw new NotImplementedException();
    }

    public Task<TNodeRecord> FindByNameAsync(string name)
    {
        throw new NotImplementedException();
    }

    public IList<TNodeRecord> FindChildren(string name)
    {
        throw new NotImplementedException();
    }

    public Task<IList<TNodeRecord>> FindChildrenAsync(string name)
    {
        throw new NotImplementedException();
    }
}

using UpscaleDown.EventDriven.Architecture.Interfaces.Services;
using UpscaleDown.EventDriven.Core.Interfaces.Entities;
using UpscaleDown.EventDriven.Repository.Interfaces;

namespace UpscaleDown.EventDriven.Architecture.Services;

public class NodeService<TNodeRecord> : RecordService<TNodeRecord>, INodeService<TNodeRecord> where TNodeRecord : INodeRecord
{
    public NodeService(IRecordRepository<TNodeRecord> repository) : base(repository)
    {
    }

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

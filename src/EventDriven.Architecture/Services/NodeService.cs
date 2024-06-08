using UpscaleDown.EventDriven.Architecture.Interfaces.Services;
using UpscaleDown.EventDriven.Repository.Interfaces.Entities;
using UpscaleDown.EventDriven.Events;
using UpscaleDown.EventDriven.Repository.Interfaces;
using UpscaleDown.EventDriven.Events.Interfaces;

namespace UpscaleDown.EventDriven.Architecture.Services;

public class NodeService<TNodeRecord> : RecordService<TNodeRecord>, INodeService<TNodeRecord> where TNodeRecord : INodeRecord
{
    public NodeService(IRecordRepository<TNodeRecord> repository, IEventPublisher<TNodeRecord> publisher = null!) : base(repository, publisher)
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

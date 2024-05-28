using MongoDB.Driver;
using UpscaleDown.EventDriven.Architecture.Configuration;
using UpscaleDown.EventDriven.Core.Interfaces.Entities;
using UpscaleDown.EventDriven.Repository.Interfaces;

namespace UpscaleDown.EventDriven.Providers.Data.Mongo;

public class MongoNodeRepository<T> : MongoRecordRepository<T>, INodeRepository<T> where T : INodeRecord
{
    public string Resource { get; private set; }
    public MongoNodeRepository(MongoClient client, EventDrivenOptions options) : base(client, options)
    {
    }

    public T FindByName(string name)
    {
        throw new NotImplementedException();
    }

    public Task<T> FindByNameAsync(string name)
    {
        throw new NotImplementedException();
    }

    public IList<T> FindChildren(string name)
    {
        throw new NotImplementedException();
    }

    public Task<IList<T>> FindChildrenAsync(string name)
    {
        throw new NotImplementedException();
    }
}
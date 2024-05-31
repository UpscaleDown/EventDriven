using MongoDB.Driver;
using UpscaleDown.EventDriven.Architecture.Configuration;
using UpscaleDown.EventDriven.Repository.Builders;
using UpscaleDown.EventDriven.Repository.Interfaces.Entities;
using UpscaleDown.EventDriven.Repository.Query;
using UpscaleDown.EventDriven.Repository.Interfaces;

namespace UpscaleDown.EventDriven.Providers.Data.Mongo;

public class MongoRecordRepository<T> : IRecordRepository<T> where T : IRecord
{
    public string Resource { get; private set; }

    protected readonly IMongoDatabase _database;
    protected readonly IMongoCollection<T> _collection;
    public MongoRecordRepository(MongoClient client, EventDrivenOptions options)
    {
        Resource = ResourceBuilder
        .Provider(options.Provider)
        .Origin(options.Origin)
        .Entity<T>()
        .Build();

        _database = client.GetDatabase(options.Origin.ToLowerInvariant());
        _collection = _database.GetCollection<T>(typeof(T).Name.ToLowerInvariant());

    }
    public IList<T> AddMany(IEnumerable<T> records)
    {
        throw new NotImplementedException();
    }

    public Task<IList<T>> AddManyAsync(IEnumerable<T> records)
    {
        throw new NotImplementedException();
    }

    public T AddOne(T record)
    {
        throw new NotImplementedException();
    }

    public Task<T> AddOneAsync(T record)
    {
        throw new NotImplementedException();
    }

    public void DeleteMany(IEnumerable<T> records)
    {
        throw new NotImplementedException();
    }

    public void DeleteMany(IEnumerable<string> ids)
    {
        throw new NotImplementedException();
    }

    public Task DeleteManyAsync(IEnumerable<T> records)
    {
        throw new NotImplementedException();
    }

    public Task DeleteManyAsync(IEnumerable<string> ids)
    {
        throw new NotImplementedException();
    }

    public void DeleteOne(T record)
    {
        throw new NotImplementedException();
    }

    public void DeleteOne(string id)
    {
        throw new NotImplementedException();
    }

    public Task DeleteOneAsync(T record)
    {
        throw new NotImplementedException();
    }

    public Task DeleteOneAsync(string id)
    {
        throw new NotImplementedException();
    }

    public IList<T> FindAll(Filters? filters = null, Pagination? pagination = null)
    {
        throw new NotImplementedException();
    }

    public Task<IList<T>> FindAllAsync(Filters? filters = null, Pagination? pagination = null)
    {
        throw new NotImplementedException();
    }

    public T FindById(string id)
    {
        throw new NotImplementedException();
    }

    public Task<T> FindByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public IList<T> UpdateMany(IEnumerable<T> records)
    {
        throw new NotImplementedException();
    }

    public Task<IList<T>> UpdateManyAsync(IEnumerable<T> records)
    {
        throw new NotImplementedException();
    }

    public T UpdateOne(T record)
    {
        throw new NotImplementedException();
    }

    public Task<T> UpdateOneAsync()
    {
        throw new NotImplementedException();
    }
}
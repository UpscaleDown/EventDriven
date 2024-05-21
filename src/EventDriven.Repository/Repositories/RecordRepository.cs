using UpscaleDown.EventDriven.Core.Builders;
using UpscaleDown.EventDriven.Core.Interfaces.Entities;
using UpscaleDown.EventDriven.Core.Query;
using UpscaleDown.EventDriven.Repository.Interfaces;

namespace UpscaleDown.EventDriven.Repository;

public class RecordRepository<TRecord> : IRecordRepository<TRecord> where TRecord : IRecord
{
    public string Resource => ResourceBuilder.Provider("server").Origin("???").Entity<TRecord>().Build();

    #region Interface Methods
    public IList<TRecord> AddMany(IEnumerable<TRecord> records)
    {
        throw new NotImplementedException();
    }

    public Task<IList<TRecord>> AddManyAsync(IEnumerable<TRecord> records)
    {
        throw new NotImplementedException();
    }

    public TRecord AddOne(TRecord record)
    {
        throw new NotImplementedException();
    }

    public Task<TRecord> AddOneAsync(TRecord record)
    {
        throw new NotImplementedException();
    }

    public void DeleteMany(IEnumerable<TRecord> records)
    {
        throw new NotImplementedException();
    }

    public void DeleteMany(IEnumerable<string> ids)
    {
        throw new NotImplementedException();
    }

    public Task DeleteManyAsync(IEnumerable<TRecord> records)
    {
        throw new NotImplementedException();
    }

    public Task DeleteManyAsync(IEnumerable<string> ids)
    {
        throw new NotImplementedException();
    }

    public void DeleteOne(TRecord record)
    {
        throw new NotImplementedException();
    }

    public void DeleteOne(string id)
    {
        throw new NotImplementedException();
    }

    public Task DeleteOneAsync(TRecord record)
    {
        throw new NotImplementedException();
    }

    public Task DeleteOneAsync(string id)
    {
        throw new NotImplementedException();
    }

    public IList<TRecord> FindAll(Filters? filters = null)
    {
        throw new NotImplementedException();
    }

    public Task<IList<TRecord>> FindAllAsync(Filters? filters = null)
    {
        throw new NotImplementedException();
    }

    public TRecord FindById(string id)
    {
        throw new NotImplementedException();
    }

    public Task<TRecord> FindByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public IList<TRecord> UpdateMany(IEnumerable<TRecord> records)
    {
        throw new NotImplementedException();
    }

    public Task<IList<TRecord>> UpdateManyAsync(IEnumerable<TRecord> records)
    {
        throw new NotImplementedException();
    }

    public TRecord UpdateOne(TRecord record)
    {
        throw new NotImplementedException();
    }

    public Task<TRecord> UpdateOneAsync()
    {
        throw new NotImplementedException();
    }
    #endregion

}

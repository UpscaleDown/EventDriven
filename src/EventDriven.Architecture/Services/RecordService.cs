using UpscaleDown.EventDriven.Architecture.Interfaces.Services;
using UpscaleDown.EventDriven.Core.Interfaces.Entities;
using UpscaleDown.EventDriven.Core.Query;
using UpscaleDown.EventDriven.Repository.Interfaces;

namespace UpscaleDown.EventDriven.Architecture.Services;

public class RecordService<TRecord> : IRecordService<TRecord> where TRecord : IRecord
{
    #region Private Members
    private readonly IRecordRepository<TRecord> _repository;
    #endregion

    #region Constructors
    public RecordService(IRecordRepository<TRecord> repository)
    {
        _repository = repository;
    }
    #endregion

    #region Interface Methods
    public async Task<IList<TRecord>> AddManyAsync(IEnumerable<TRecord> records)
    {
        throw new NotImplementedException();
    }

    public async Task<TRecord> AddOneAsync(TRecord record)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteManyAsync(IEnumerable<TRecord> records)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteManyAsync(IEnumerable<string> ids)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteOneAsync(TRecord record)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteOneAsync(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<IList<TRecord>> FindAllAsync(Filters? filters = null)
    {
        throw new NotImplementedException();
    }

    public async Task<TRecord> FindByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<IList<TRecord>> UpdateManyAsync(IEnumerable<TRecord> records)
    {
        throw new NotImplementedException();
    }

    public async Task<TRecord> UpdateOneAsync()
    {
        throw new NotImplementedException();
    }
    #endregion

}

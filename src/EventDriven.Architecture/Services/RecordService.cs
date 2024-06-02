using UpscaleDown.EventDriven.Architecture.Interfaces.Services;
using UpscaleDown.EventDriven.Repository.Interfaces.Entities;
using UpscaleDown.EventDriven.Repository.Query;
using UpscaleDown.EventDriven.Events;
using UpscaleDown.EventDriven.Repository.Interfaces;
using UpscaleDown.EventDriven.Events.Interfaces;

namespace UpscaleDown.EventDriven.Architecture.Services;

public class RecordService<TRecord> : IRecordService<TRecord> where TRecord : IRecord
{
    #region Private Members
    protected readonly IRecordRepository<TRecord> _repository;
    protected readonly IEventPublisher<TRecord> _publisher;
    #endregion

    #region Constructors
    public RecordService(IRecordRepository<TRecord> repository, IEventPublisher<TRecord> publisher = null!)
    {
        _repository = repository;
        _publisher = publisher;
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

    #region Private methods
    private void PublishEvent(string type, TRecord data)
    {
        if (_publisher == null) return;
    }
    #endregion
}

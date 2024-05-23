using UpscaleDown.EventDriven.Core.Interfaces.Entities;
using UpscaleDown.EventDriven.Core.Query;

namespace UpscaleDown.EventDriven.Repository.Interfaces;

public interface IRecordRepository<T> where T : IRecord
{
    public string Resource { get; }
    public T FindById(string id);
    public Task<T> FindByIdAsync(string id);
    public IList<T> FindAll(Filters? filters = null, Pagination? pagination = null);
    public Task<IList<T>> FindAllAsync(Filters? filters = null, Pagination? pagination = null);
    public T AddOne(T record);
    public Task<T> AddOneAsync(T record);
    public IList<T> AddMany(IEnumerable<T> records);
    public Task<IList<T>> AddManyAsync(IEnumerable<T> records);
    public T UpdateOne(T record);
    public Task<T> UpdateOneAsync();
    public IList<T> UpdateMany(IEnumerable<T> records);
    public Task<IList<T>> UpdateManyAsync(IEnumerable<T> records);
    public void DeleteOne(T record);
    public Task DeleteOneAsync(T record);
    public void DeleteOne(string id);
    public Task DeleteOneAsync(string id);
    public void DeleteMany(IEnumerable<T> records);
    public Task DeleteManyAsync(IEnumerable<T> records);
    public void DeleteMany(IEnumerable<string> ids);
    public Task DeleteManyAsync(IEnumerable<string> ids);
}
using UpscaleDown.EventDriven.Core.Interfaces.Entities;
using UpscaleDown.EventDriven.Core.Query;

namespace UpscaleDown.EventDriven.Architecture.Interfaces.Services;

public interface IRecordService<T> where T : IRecord
{
    public Task<T> FindByIdAsync(string id);
    public Task<IList<T>> FindAllAsync(Filters? filters = null);
    public Task<T> AddOneAsync(T record);
    public Task<IList<T>> AddManyAsync(IEnumerable<T> records);
    public Task<T> UpdateOneAsync();
    public Task<IList<T>> UpdateManyAsync(IEnumerable<T> records);
    public Task DeleteOneAsync(T record);
    public Task DeleteOneAsync(string id);
    public Task DeleteManyAsync(IEnumerable<T> records);
    public Task DeleteManyAsync(IEnumerable<string> ids);
}
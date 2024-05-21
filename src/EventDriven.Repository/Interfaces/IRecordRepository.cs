using UpscaleDown.EventDriven.Core.Interfaces.Entities;

namespace UpscaleDown.EventDriven.Repository.Interfaces;

public interface IRecordRepository<T> where T : IRecord
{
    public string Resource { get; }
    public T FindById(string id);
    public Task<T> FindByIdAsync(string id);
}
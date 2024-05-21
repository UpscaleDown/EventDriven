using UpscaleDown.EventDriven.Core.Interfaces.Entities;

namespace UpscaleDown.EventDriven.Repository.Interfaces;

public interface INodeRepository<T> : IRecordRepository<T> where T : INodeRecord
{
    public T FindByName(string name);
    public Task<T> FindByNameAsync(string name);
    public IList<T> FindChildren(string name);
    public Task<IList<T>> FindChildrenAsync(string name);
}
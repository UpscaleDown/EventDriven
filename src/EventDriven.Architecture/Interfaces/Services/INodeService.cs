using UpscaleDown.EventDriven.Repository.Interfaces.Entities;

namespace UpscaleDown.EventDriven.Architecture.Interfaces.Services;

public interface INodeService<T> : IRecordService<T> where T : INodeRecord
{
    public T FindByName(string name);
    public Task<T> FindByNameAsync(string name);
    public IList<T> FindChildren(string name);
    public Task<IList<T>> FindChildrenAsync(string name);
}
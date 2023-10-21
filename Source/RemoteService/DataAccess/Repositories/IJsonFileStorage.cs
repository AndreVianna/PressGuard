namespace RemoteService.Repositories;

public interface IJsonFileStorage<TData>
    where TData : class, IPersisted {
    void SetBasePath(string path);
    Task<IEnumerable<TData>> GetAllAsync(Func<TData, bool>? filter = null, CancellationToken ct = default);
    Task<TData?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<TData?> CreateAsync(TData data, CancellationToken ct = default);
    Task<TData?> UpdateAsync(TData data, CancellationToken ct = default);
    bool Delete(Guid id);
}

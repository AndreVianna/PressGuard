using RemoteService.Models;
using RemoteService.Models.Abstractions;

namespace RemoteService.Repositories;

public interface IRepository<TModel, TRowModel>
    where TModel : class, IPersisted
    where TRowModel : Row {
    Task<IEnumerable<TRowModel>> GetManyAsync(CancellationToken ct = default);
    Task<TModel?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<TModel?> AddAsync(TModel input, CancellationToken ct = default);
    Task<TModel?> UpdateAsync(TModel input, CancellationToken ct = default);
    Task<bool> RemoveAsync(Guid id, CancellationToken ct = default);
}
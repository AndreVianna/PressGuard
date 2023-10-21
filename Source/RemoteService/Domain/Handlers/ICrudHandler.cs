namespace RemoteService.Handlers;

public interface ICrudHandler<TModel, TRowModel>
    where TModel : Persisted, IValidatable
    where TRowModel : Row {
    Task<CrudResult<IEnumerable<TRowModel>>> GetManyAsync(CancellationToken ct = default);
    Task<CrudResult<TModel>> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<CrudResult<TModel>> AddAsync(TModel input, CancellationToken ct = default);
    Task<CrudResult<TModel>> UpdateAsync(TModel input, CancellationToken ct = default);
    Task<CrudResult> RemoveAsync(Guid id, CancellationToken ct = default);
}

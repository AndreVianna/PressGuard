using RemoteService.Models;
using RemoteService.Models.Abstractions;
using RemoteService.Repositories;

using static System.Results.CrudResult;

namespace RemoteService.Handlers;

public class CrudHandler<TModel, TRowModel, TRepository>
    : ICrudHandler<TModel, TRowModel>
    where TModel : class, IPersisted, IValidatable
    where TRowModel : Row
    where TRepository : IRepository<TModel, TRowModel> {

    public CrudHandler(TRepository repository) {
        Repository = repository;
    }

    protected TRepository Repository { get; }

    public virtual async Task<CrudResult<IEnumerable<TRowModel>>> GetManyAsync(CancellationToken ct = default) {
        var list = await Repository.GetManyAsync(ct).ConfigureAwait(false);
        return Success(list);
    }

    public virtual async Task<CrudResult<TModel>> GetByIdAsync(Guid id, CancellationToken ct = default) {
        var output = await Repository.GetByIdAsync(id, ct).ConfigureAwait(false);
        return output is not null
            ? Success(output)
            : NotFound<TModel>();
    }

    public virtual async Task<CrudResult<TModel>> AddAsync(TModel input, CancellationToken ct = default) {
        var validation = input.Validate();
        if (validation.IsInvalid) return Invalid(input, validation.Errors);

        var beforeResult = await OnAddingAsync(input, ct).ConfigureAwait(false);
        if (beforeResult.IsInvalid) return Invalid(input, beforeResult.Errors);

        var output = await Repository.AddAsync(input, ct).ConfigureAwait(false);

        if (output != null) await OnAddedAsync(input, ct).ConfigureAwait(false);
        return output is null
            ? Conflict(input)
            : Success(output);
    }

    protected virtual Task<Result> OnAddingAsync(TModel input, CancellationToken ct = default) => Task.FromResult(Result.Success());
    protected virtual Task OnAddedAsync(TModel input, CancellationToken ct = default) => Task.CompletedTask;

    public virtual async Task<CrudResult<TModel>> UpdateAsync(TModel input, CancellationToken ct = default) {
        var validation = input.Validate();
        if (validation.IsInvalid) return Invalid(input, validation.Errors);

        var original = await Repository.GetByIdAsync(input.Id, ct).ConfigureAwait(false);
        if (original is null) return NotFound<TModel>();

        var beforeResult = await OnUpdatingAsync(original, input, ct).ConfigureAwait(false);
        if (beforeResult.IsInvalid) return Invalid(input, beforeResult.Errors);

        var output = await Repository.UpdateAsync(input, ct).ConfigureAwait(false);

        await OnUpdatedAsync(input, ct).ConfigureAwait(false);
        return Success(output!);
    }

    protected virtual Task<Result> OnUpdatingAsync(TModel original, TModel updated, CancellationToken ct = default) => Task.FromResult(Result.Success());
    protected virtual Task OnUpdatedAsync(TModel input, CancellationToken ct = default) => Task.CompletedTask;

    public virtual async Task<CrudResult> RemoveAsync(Guid id, CancellationToken ct = default) {
        var original = await Repository.GetByIdAsync(id, ct).ConfigureAwait(false);
        if (original is null) return NotFound();

        var beforeResult = await OnRemovingAsync(original, ct).ConfigureAwait(false);
        if (beforeResult.IsInvalid) return Invalid(original, beforeResult.Errors);

        await Repository.RemoveAsync(id, ct);

        await OnRemovedAsync(original, ct).ConfigureAwait(false);

        return Success();
    }

    protected virtual Task<Result> OnRemovingAsync(TModel input, CancellationToken ct = default) => Task.FromResult(Result.Success());
    protected virtual Task OnRemovedAsync(TModel input, CancellationToken ct = default) => Task.CompletedTask;

}

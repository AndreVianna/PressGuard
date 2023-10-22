using RemoteService.Handlers.Tenants;
using RemoteService.Identity;

namespace RemoteService.Repositories.Tenants;

public class TenantRepository : ITenantRepository {
    private readonly IJsonFileStorage<TenantData> _files;

    public TenantRepository(IJsonFileStorage<TenantData> files, IUserAccessor owner) {
        _files = files;
        files.SetBasePath($"{owner.BaseFolder}/Systems");
    }

    public async Task<IEnumerable<TenantRow>> GetManyAsync(CancellationToken ct = default) {
        var files = await _files
            .GetAllAsync(ct: ct)
            .ConfigureAwait(false);
        return files.ToArray(Mapper.ToRow);
    }

    public async Task<Tenant?> GetByIdAsync(Guid id, CancellationToken ct = default) {
        var file = await _files
            .GetByIdAsync(id, ct)
            .ConfigureAwait(false);
        return Mapper.ToModel(file);
    }

    public async Task<Tenant?> AddAsync(Tenant input, CancellationToken ct = default) {
        var file = await _files.CreateAsync(Mapper.ToData(input), ct).ConfigureAwait(false);
        return Mapper.ToModel(file);
    }

    public async Task<Tenant?> UpdateAsync(Tenant input, CancellationToken ct = default) {
        var file = await _files.UpdateAsync(Mapper.ToData(input), ct);
        return Mapper.ToModel(file);
    }

    public Task<bool> RemoveAsync(Guid id, CancellationToken ct = default)
        => Task.Run(() => _files.Delete(id), ct);
}

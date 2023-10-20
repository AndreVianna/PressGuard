using DataAccess.Identity;

namespace DataAccess.Repositories.Settings;

public class SettingRepository : ISettingRepository {
    private readonly IJsonFileStorage<SettingData> _files;

    public SettingRepository(IJsonFileStorage<SettingData> files, IUserAccessor owner) {
        _files = files;
        files.SetBasePath($"{owner.BaseFolder}/Settings");
    }

    public async Task<IEnumerable<SettingRow>> GetManyAsync(CancellationToken ct = default) {
        var files = await _files
            .GetAllAsync(ct: ct)
            .ConfigureAwait(false);
        return files.ToArray(SettingMapper.ToRow);
    }

    public async Task<Setting?> GetByIdAsync(Guid id, CancellationToken ct = default) {
        var file = await _files
            .GetByIdAsync(id, ct)
            .ConfigureAwait(false);
        return file.ToModel();
    }

    public async Task<Setting?> AddAsync(Setting input, CancellationToken ct = default) {
        var file = await _files.CreateAsync(input.ToData(), ct).ConfigureAwait(false);
        return file.ToModel();
    }

    public async Task<Setting?> UpdateAsync(Setting input, CancellationToken ct = default) {
        var file = await _files.UpdateAsync(input.ToData(), ct);
        return file.ToModel();
    }

    public Task<bool> RemoveAsync(Guid id, CancellationToken ct = default)
        => Task.Run(() => _files.Delete(id), ct);
}

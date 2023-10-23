using RemoteService.Handlers.Venues;
using RemoteService.Identity;

namespace RemoteService.Repositories.Venues;

public class VenueRepository : IVenueRepository {
    private readonly IJsonFileStorage<VenueData> _files;

    public VenueRepository(IJsonFileStorage<VenueData> files, IUserAccessor owner) {
        _files = files;
        files.SetBasePath($"{owner.BaseFolder}/Settings");
    }

    public async Task<IEnumerable<VenueRow>> GetManyAsync(CancellationToken ct = default) {
        var files = await _files
            .GetAllAsync(ct: ct)
            .ConfigureAwait(false);
        return files.ToArray(Mapper.ToRow);
    }

    public async Task<Venue?> GetByIdAsync(Guid id, CancellationToken ct = default) {
        var file = await _files
            .GetByIdAsync(id, ct)
            .ConfigureAwait(false);
        return file.ToModel();
    }

    public async Task<Venue?> AddAsync(Venue input, CancellationToken ct = default) {
        var file = await _files.CreateAsync(input.ToData(), ct).ConfigureAwait(false);
        return file.ToModel();
    }

    public async Task<Venue?> UpdateAsync(Venue input, CancellationToken ct = default) {
        var file = await _files.UpdateAsync(input.ToData(), ct);
        return file.ToModel();
    }

    public Task<bool> RemoveAsync(Guid id, CancellationToken ct = default)
        => Task.Run(() => _files.Delete(id), ct);
}

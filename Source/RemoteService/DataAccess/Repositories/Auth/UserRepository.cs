namespace RemoteService.Repositories.Auth;

public class UserRepository : IUserRepository {
    private static bool _emailIndexLoaded;
    private static IDictionary<string, Guid> _emailIndex = null!;

    private readonly IJsonFileStorage<UserData> _users;
    private readonly IHasher _hasher;

    public UserRepository(IJsonFileStorage<UserData> users, IHasher hasher) {
        _users = users;
        _hasher = hasher;
        users.SetBasePath("Users");
        LoadEmailIndex();
    }

    private void LoadEmailIndex() {
        if (_emailIndexLoaded) return;
        var users = _users.GetAllAsync().Result;
        _emailIndex = users.ToDictionary(i => i.Email, i => i.Id);
        _emailIndexLoaded = true;
    }

    public async Task<IEnumerable<UserRow>> GetManyAsync(CancellationToken ct = default) {
        var users = await _users
            .GetAllAsync(ct: ct)
            .ConfigureAwait(false);
        return users.ToArray(UserMapper.ToRow);
    }

    public async Task<Handlers.Auth.User?> GetByIdAsync(Guid id, CancellationToken ct = default) {
        var userData = await _users
            .GetByIdAsync(id, ct)
            .ConfigureAwait(false);
        return userData.ToModel();
    }

    public async Task<Handlers.Auth.User?> AddAsync(Handlers.Auth.User input, CancellationToken ct = default) {
        if (_emailIndex.ContainsKey(input.Email.ToUpperInvariant())) return default; 
        var userData = await _users.CreateAsync(input.ToData(), ct).ConfigureAwait(false);
        if (userData is not null) _emailIndex[userData.Email.ToUpperInvariant()] = userData.Id;
        return userData.ToModel();
    }

    public async Task<Handlers.Auth.User?> UpdateAsync(Handlers.Auth.User input, CancellationToken ct = default) {
        var userData = await _users.UpdateAsync(input.ToData(), ct);
        return userData.ToModel();
    }

    public Task<bool> RemoveAsync(Guid id, CancellationToken ct = default)
        => Task.Run(() => {
                var isDeleted = _users.Delete(id);
                if (isDeleted) _emailIndex.Remove(_emailIndex.First(i => i.Value == id).Key);
                return isDeleted;
            }, ct);

    public async Task<Handlers.Auth.User?> VerifyAsync(SignIn signIn, CancellationToken ct) {
        var user = await GetByEmailAsync(signIn.Email, ct).ConfigureAwait(false);
        return user?.HashedPassword?.Verify(signIn.Password, _hasher) ?? false
            ? user
            : default;
    }

    private async Task<Handlers.Auth.User?> GetByEmailAsync(string email, CancellationToken ct = default)
        => _emailIndex.ContainsKey(email)
            ? await GetByIdAsync(_emailIndex[email], ct)
            : default;
}

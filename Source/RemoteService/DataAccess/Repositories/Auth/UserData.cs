using RemoteService.Authentication;

namespace RemoteService.Repositories.Auth;

public record UserData : Persisted {
    public required string Email { get; init; }
    public HashedSecret? HashedPassword { get; init; }
    public DateTime LockExpiration { get; init; } = DateTime.MinValue;
    public int SignInRetryCount { get; init; }
    public bool IsBlocked { get; init; }
    public ICollection<Role> Roles { get; init; } = Array.Empty<Role>();

    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public DateOnly? Birthday { get; init; }
}

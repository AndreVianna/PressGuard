namespace DataAccess.Repositories.Auth;

public record UserData : IPersisted {
    public Guid Id { get; init; }
    public required string Email { get; init; }
    public HashedSecret? HashedPassword { get; init; }
    public DateTime LockExpiration { get; init; } = DateTime.MinValue;
    public int SignInRetryCount { get; init; }
    public bool IsBlocked { get; init; }
    public ICollection<Role> Roles { get; init; } = Array.Empty<Role>();

    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public DateOnly? Birthday { get; init; }

    public DateTime ChangeStamp { get; init; }
}

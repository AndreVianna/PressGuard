using RemoteService.Handlers.Auth;
using RemoteService.Models.Abstractions;

namespace RemoteService.Authentication;

public record User : IValidatable, IPersisted {
    public required Guid Id { get; init; }
    public required string Email { get; init; }
    public bool IsConfirmed { get; init; }
    public HashedSecret? HashedPassword { get; init; }
    public DateTime LockExpiration { get; init; } = DateTime.MinValue;
    public int SignInRetryCount { get; init; }
    public bool IsBlocked { get; init; }
    public ICollection<Role> Roles { get; init; } = new HashSet<Role>();

    [PersonalInformation]
    public string? FirstName { get; init; }
    [PersonalInformation]
    public string? LastName { get; init; }
    [PersonalInformation]
    public DateOnly? Birthday { get; init; }

    public DateTime ChangeStamp { get; init; }

    public Result Validate(IDictionary<string, object?>? context = null) {
        var result = Result.Success();
        result += Email.IsRequired()
            .And().IsNotEmptyOrWhiteSpace()
            .And().IsEmail().Result;
        return result;
    }
}

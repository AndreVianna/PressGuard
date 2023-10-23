namespace RemoteService.Models;

public record Tenant : PersistedBase {
    public ICollection<UserRole> Permissions { get; init; } = new List<UserRole>();
    public ICollection<Venue> Venues { get; init; } = new List<Venue>();

    public override Result Validate(IDictionary<string, object?>? context = null) {
        var result = base.Validate(context);
        result += Venues!.CheckIfEach(item => item.IsRequired().And().IsValid()).Result;
        return result;
    }
}

namespace RemoteService.Models;

public record PersistedBase : Persisted, IBase {
    public required string Name { get; init; }
    public required string Description { get; init; }

    public virtual Result Validate(IDictionary<string, object?>? context = null) {
        var result = Result.Success();
        result += Name.IsRequired()
            .And().IsNotEmptyOrWhiteSpace()
            .And().LengthIsAtMost(Validation.Name.MaximumLength).Result;
        result += Description.IsRequired()
            .And().IsNotEmptyOrWhiteSpace()
            .And().LengthIsAtMost(Validation.Description.MaximumLength).Result;
        return result;
    }
}

namespace RemoteService.Models;

public record Address : IValidatable {
    public required string Line1 { get; init; }
    public string? Line2 { get; init; }
    public string? City { get; init; }
    public string? Province { get; init; }
    public required string ZipCode { get; init; }

    public virtual Result Validate(IDictionary<string, object?>? context = null) {
        var result = Result.Success();
        result += Line1.IsRequired().Result;
        result += ZipCode.IsRequired().Result;
        return result;
    }
}

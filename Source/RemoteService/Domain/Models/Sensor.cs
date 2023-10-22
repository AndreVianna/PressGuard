namespace RemoteService.Models;

public record Sensor : IValidatable {
    public required string Model { get; init; }
    public Result Validate(IDictionary<string, object?>? context = null) {
        var result = Result.Success();
        result += Model.IsRequired().And().IsNotEmptyOrWhiteSpace().Result;
        return result;
    }
}

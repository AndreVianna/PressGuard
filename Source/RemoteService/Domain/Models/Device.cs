namespace RemoteService.Models;

public record Device : Base {
    public required int Port { get; init; }
    public ICollection<Sensor> Sensors { get; init; } = new List<Sensor>();

    public override Result Validate(IDictionary<string, object?>? context = null) {
        var result = base.Validate(context);
        result += Sensors!.CheckIfEach(item => item.IsRequired().And().IsValid()).Result;
        return result;
    }
}

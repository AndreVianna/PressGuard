namespace RemoteService.Models;

public record Venue : PersistedBase {
    public required Address Address { get; init; }

    public ICollection<Device> Devices { get; init; } = new List<Device>();

    public override Result Validate(IDictionary<string, object?>? context = null) {
        var result = base.Validate(context);
        result += Address.IsRequired().And().IsValid().Result;
        result += Devices!.CheckIfEach(item => item.IsRequired().And().IsValid()).Result;
        return result;
    }
}

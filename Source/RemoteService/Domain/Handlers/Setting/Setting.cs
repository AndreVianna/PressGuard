namespace RemoteService.Handlers.Setting;

public record Setting : PersistedBase {
    public ICollection<Base> Components { get; init; } = new List<Base>();
    public ICollection<AttributeDefinition> AttributeDefinitions { get; init; } = new List<AttributeDefinition>();

    public override Result Validate(IDictionary<string, object?>? context = null) {
        var result = base.Validate(context);
        result += Components!.CheckIfEach(item => item.IsRequired().And().IsValid()).Result;
        result += AttributeDefinitions!.CheckIfEach(item => item.IsRequired().And().IsValid()).Result;
        return result;
    }
}

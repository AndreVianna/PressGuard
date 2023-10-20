using RemoteService.Models.Abstractions;

namespace RemoteService.Models.Attributes;

public abstract record Attribute<TValue> : IAttribute {
    public required AttributeDefinition Definition { get; init; }

    object? IAttribute.Value => Value;
    public TValue Value { get; init; } = default!;

    public Result Validate(IDictionary<string, object?>? context = null) {
        var result = Result.Success();
        result += Definition.DataType.Is().And().IsEqualTo<TValue>().Result;
        result += Definition.Constraints.Aggregate(Result.Success(), (r, c)
            => r + c.Create<TValue>(Definition.Name).Validate(Value));
        return result;
    }
}

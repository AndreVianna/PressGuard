namespace RemoteService.Models.Attributes;

public record AttributeDefinition : Base, IAttributeDefinition {
    public required Type DataType { get; init; }

    public ICollection<AttributeConstraint> Constraints { get; } = new List<AttributeConstraint>();
}

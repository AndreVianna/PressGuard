using RemoteService.Models.Attributes;

namespace RemoteService.Models.Abstractions;

public interface IAttributeDefinition : IBase {
    Type DataType { get; }
    ICollection<AttributeConstraint> Constraints { get; }
}
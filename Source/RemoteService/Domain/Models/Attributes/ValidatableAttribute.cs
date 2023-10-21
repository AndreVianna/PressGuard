namespace RemoteService.Models.Attributes;

public record ValidatableAttribute<TValue>
    : Attribute<TValue>
    where TValue : class, IValidatable;
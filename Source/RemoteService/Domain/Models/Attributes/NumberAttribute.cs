namespace RemoteService.Models.Attributes;

public record NumberAttribute<TValue>
    : Attribute<TValue>
    where TValue : IComparable<TValue>;
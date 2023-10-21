namespace RemoteService.Models.Attributes;

public record ListAttribute<TValue>
    : Attribute<List<TValue>>;
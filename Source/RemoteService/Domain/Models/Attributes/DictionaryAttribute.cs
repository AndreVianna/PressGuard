namespace RemoteService.Models.Attributes;

public record DictionaryAttribute<TKey, TValue>
    : Attribute<Dictionary<TKey, TValue>>
    where TKey : notnull;
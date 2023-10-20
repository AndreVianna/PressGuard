namespace System.Extensions;

public static class DictionaryExtensions {
    public static IConnector<DictionaryValidator<TKey, TValue>> Is<TKey, TValue>(this IDictionary<TKey, TValue?>? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        where TKey : notnull
        => Create(subject, source!);
    public static IConnector<DictionaryValidator<TKey, TValue>> CheckIfEach<TKey, TValue>(this IDictionary<TKey, TValue?>? subject, Func<TValue?, ITerminator> validate, [CallerArgumentExpression(nameof(subject))] string? source = null)
        where TKey : notnull
        => Create(subject, source!, validate);
}
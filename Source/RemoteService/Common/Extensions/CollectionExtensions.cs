namespace System.Extensions;

public static class CollectionExtensions {
    public static IConnector<CollectionValidator<TItem>> Is<TItem>(this ICollection<TItem?> subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => Create(subject, source!);
    public static IConnector<CollectionValidator<TItem>> CheckIfEach<TItem>(this ICollection<TItem?> subject, Func<TItem?, ITerminator> validateItem, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => Create(subject, source!, validateItem);
}

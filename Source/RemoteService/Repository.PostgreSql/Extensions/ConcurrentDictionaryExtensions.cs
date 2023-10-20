﻿namespace Repository.PostgreSql.Extensions;

internal static class ConcurrentDictionaryExtensions {
    public static TValue? Get<TKey, TValue>(this ConcurrentDictionary<TKey, object?> cache, TKey key, Func<TKey, TValue?> getValue)
        where TKey : notnull
        where TValue : class
        => (TValue?)cache.GetOrAdd(key, getValue);
}

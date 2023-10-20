namespace System.Extensions;

public static class TypeExtensions {
    public static IConnector<TypeValidator> Is(this Type? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => Create(subject, source!);

    public static string GetName(this Type type) {
        if (type is { IsGenericType: false, IsArray: false })
            return GetNonGenericTypeName(type.Name);
        if (type.IsArray)
            return $"{type.GetElementType()!.GetName()}[]";
        var genericArgs = string.Join(",", type.GetGenericArguments().Select(GetName));
        var collectionType = type.Name.Split('`')[0];
        return collectionType switch {
            "List" or "ICollection" => $"List<{genericArgs}>",
            "Dictionary" or "IDictionary" => $"Dictionary<{genericArgs}>",
            _ => $"{collectionType}<{genericArgs}>"
        };
    }

    private static string GetNonGenericTypeName(string name)
        => name switch {
            "Int32" => "Integer",
            "String" => "String",
            "Decimal" => "Decimal",
            _ => name
        };
}
namespace System.Utilities;

public static class Create {
    public static T Instance<T>(params object?[]? args)
        where T : class
        => (T)Activator.CreateInstance(typeof(T), args)!;

    public static Type TypeFrom(string name)
        => name switch {
            "Integer[]" => Type.GetType("System.Int32[]")!,
            "Integer" => Type.GetType("System.Int32")!,
            "Decimal[]" => Type.GetType("System.Decimal[]")!,
            "Decimal" => Type.GetType("System.Decimal")!,
            "String[]" => Type.GetType("System.String[]")!,
            "String" => Type.GetType("System.String")!,
            _ when name.StartsWith("List<") => typeof(List<>).MakeGenericType(GetInnerTypes(name[5..^1])),
            _ when name.StartsWith("Dictionary<") => typeof(Dictionary<,>).MakeGenericType(GetInnerTypes(name[11..^1])),
            _ => Type.GetType($"System.{name}")
              ?? throw new InvalidOperationException($"Unsupported type '{name}'.")
        };

    private static Type[] GetInnerTypes(string list) => list.Split(',').Select(TypeFrom).ToArray();
}

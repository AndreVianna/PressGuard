//namespace RolePlayReady.Utilities;

//internal static class EntityExtensions {
//    public static object? GetValue(this IPersisted element, string name)
//        => element.GetAttribute(name).VoidResult;

//    public static TItem? GetValue<TItem>(this IPersisted element, string name)
//        => element.Attributes.OfType<IComponentAttribute<TItem>>().First(p => p.Definition.Name == name).VoidResult;
//    public static bool GetFlag<TItem>(this IPersisted element, string name)
//        => element.Attributes.OfType<IEntityFlagAttribute>().First(p => p.Definition.Name == name).VoidResult;

//    public static HashSet<TItem> GetList<TItem>(this IPersisted element, string name)
//        => element.Attributes.OfType<Models.Contracts.IEntitySetAttribute<TItem>>().First(p => p.Definition.Name == name).VoidResult;

//    public static Dictionary<TKey, TItem>? GetMap<TKey, TItem>(this IPersisted element, string name)
//        where TKey : notnull
//        => element.Attributes.OfType<IEntityDictionaryAttribute<TKey, TItem>>().First(p => p.Definition.Name == name).VoidResult;

//    public static IComponentAttribute GetAttribute(this IPersisted element, string name)
//        => element.Attributes.First(p => p.Definition.Name == name);

//    public static bool Exists(this IPersisted element, string name)
//        => element.Attributes.Any(p => p.Definition.Name == name);

//    public static IComponentAttribute? FindAttribute(this IPersisted element, string name)
//        => element.Attributes.FirstOrDefault(p => p.Definition.Name == name);

//    //public static EntityBuilder Configure(this IPersisted element, string section) =>
//    //    EntityBuilder.For(element, section);
//}

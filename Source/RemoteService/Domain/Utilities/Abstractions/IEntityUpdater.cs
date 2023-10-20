//namespace RolePlayReady.Utilities.Contracts;

//public interface IEntityUpdater {
//    public interface IMain {
//        ISetter Let(string attribute);
//        IFinishesValidation CheckIf(string attribute);
//        IConditional If(string attribute);

//        IActionConnector AddJournalEntry(string section, string title, string text);
//        IActionConnector AddJournalEntry(string section, string text);
//        IActionConnector AddTag(string tag);
//        //IActionConnector AddPowerSource(string name, string description, Action<IMain> build);
//    }

//    public interface IActionConnector {
//        IMain And();
//    }

//    public interface ILogicalConnector {
//        IConditional And();
//        IConditional And(string attribute);
//        IActionConnector Then(Action<IMain> onTrue, Action<IMain>? onFalse = null);
//    }

//    public interface ISetter {
//        IActionConnector Be<TItem>(TItem value);
//        IActionConnector Be<TItem>(Func<IPersisted, TItem> getValueFrom);

//        IActionConnector IncreaseBy<TItem>(TItem bonus) where TItem : IAdditionOperators<TItem, TItem, TItem>;
//        IActionConnector IncreaseBy<TItem>(Func<IPersisted, TItem> getBonusFrom) where TItem : IAdditionOperators<TItem, TItem, TItem>;
//        IActionConnector DecreaseBy<TItem>(TItem bonus) where TItem : ISubtractionOperators<TItem, TItem, TItem>;
//        IActionConnector DecreaseBy<TItem>(Func<IPersisted, TItem> getBonusFrom) where TItem : ISubtractionOperators<TItem, TItem, TItem>;

//        IActionConnector Have<TKey, TItem>(TKey key, TItem value) where TKey : notnull;
//        IActionConnector Have<TKey, TItem>(TKey key, Func<IPersisted, TItem> getValueFrom) where TKey : notnull;
//        IActionConnector Have<TKey, TItem>(IEnumerable<KeyValuePair<TKey, TItem>> items) where TKey : notnull;
//        IActionConnector Have<TKey, TItem>(Func<IPersisted, IEnumerable<KeyValuePair<TKey, TItem>>> getItemsFrom) where TKey : notnull;

//        IActionConnector Have<TItem>(TItem item);
//        IActionConnector Have<TItem>(Func<IPersisted, TItem> getItemFrom);
//        IActionConnector Have<TItem>(params TItem[] items);
//        IActionConnector Have<TItem>(Func<IPersisted, IEnumerable<TItem>> getItemsFrom);
//    }

//    public interface IFinishesValidation {
//        IActionConnector Contains<TItem>(TItem candidate, string message);
//        IActionConnector ContainsKey<TKey>(TKey key, string message) where TKey : notnull;
//        IActionConnector IsEqualTo<TItem>(TItem validValue, string message) where TItem : IEquatable<TItem>;
//        IActionConnector IsBetween<TItem>(TItem minimum, TItem maximum, string message) where TItem : IComparable<TItem>;
//        IActionConnector IsGreaterThan<TItem>(TItem minimum, string message) where TItem : IComparable<TItem>;
//        IActionConnector IsGreaterOrEqualTo<TItem>(TItem minimum, string message) where TItem : IComparable<TItem>;
//        IActionConnector IsLessThan<TItem>(TItem maximum, string message) where TItem : IComparable<TItem>;
//        IActionConnector IsLessOrEqualTo<TItem>(TItem maximum, string message) where TItem : IComparable<TItem>;
//    }

//    public interface IConditional {
//        ILogicalConnector Contains<TItem>(TItem candidate);
//        ILogicalConnector ContainsKey<TKey>(TKey key) where TKey : notnull;
//        ILogicalConnector IsEqualTo<TItem>(TItem validValue) where TItem : IEquatable<TItem>;
//        ILogicalConnector IsBetween<TItem>(TItem minimum, TItem maximum) where TItem : IComparable<TItem>;
//        ILogicalConnector IsGreaterThan<TItem>(TItem minimum) where TItem : IComparable<TItem>;
//        ILogicalConnector IsGreaterOrEqualTo<TItem>(TItem minimum) where TItem : IComparable<TItem>;
//        ILogicalConnector IsLessThan<TItem>(TItem maximum) where TItem : IComparable<TItem>;
//        ILogicalConnector IsLessOrEqualTo<TItem>(TItem maximum) where TItem : IComparable<TItem>;
//    }
//}

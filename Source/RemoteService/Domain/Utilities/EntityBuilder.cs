//using RolePlayReady.Utilities.Contracts;

//namespace RolePlayReady.Utilities;

//public class EntityBuilder {
//    private readonly IPersisted _entity;
//    private readonly string _section;

//    private EntityBuilder(IPersisted entity, string section) {
//        _entity = entity;
//        _section = section;
//    }

//    public static EntityBuilder For(IPersisted element, string section)
//        => new EntityBuilder(element, section);

//    public IPersisted As(Action<ISectionBuilder.IMainCommands> configure) {
//        configure(SectionBuilder.For(_entity, _section));
//        return _entity;
//    }
//}
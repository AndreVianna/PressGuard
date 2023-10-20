//using RolePlayReady.Models;
//using RolePlayReady.Utilities.Contracts;

//using Item = RolePlayReady.Models.Item;

//namespace RolePlayReady.Utilities;

//public class ComponentFactory : IEntityFactory {
//    private readonly INode _parent;
//    private readonly string _ownerId;

//    private ComponentFactory(INode parent, string ownerId) {
//        _parent = parent;
//        _ownerId = ownerId;
//    }

//    public static IEntityFactory For(INode parent, string ownerId) => new ComponentFactory(parent, ownerId);

//    public TComponent Create<TComponent>(string abbreviation, string name, string description)
//        where TComponent : INode
//        => (TComponent)Create(typeof(TComponent).Name, abbreviation, name, description);

//    public INode Create(string type, string abbreviation, string name, string description)
//        => type switch {
//            nameof(Agent) => new Agent(_parent, abbreviation, name, description),
//            //nameof(Node) => new Node(_parent, abbreviation, name, description),
//            nameof(Power) => new Power(_parent, abbreviation, name, description),
//            nameof(PowerSource) => new PowerSource(_parent, abbreviation, name, description),
//            nameof(Item) => new Item(_parent, abbreviation, name, description, string.Empty),
//            _ => throw new ArgumentException($"Unknown type: {type}", nameof(type))
//        };
//}
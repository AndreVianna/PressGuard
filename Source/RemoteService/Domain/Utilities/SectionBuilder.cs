//using RolePlayReady.Models;
//using RolePlayReady.Utilities.Contracts;

//namespace RolePlayReady.Utilities;

//public class SectionBuilder : ISectionBuilder {

//    private SectionBuilder(IPersisted entity, string section) {
//        Target = entity;
//        Section = section;
//        SectionItem = Section switch {
//            nameof(Node.Traits) => nameof(Trait),
//            nameof(Domain.PowerSources) => nameof(PowerSource),
//            _ => throw new NotImplementedException()
//        };
//    }

//    public static ISectionBuilder.IMainCommands For(IPersisted entity, string section) => new MainCommands(entity, section);

//    protected IPersisted Target { get; }
//    protected string Section { get; }
//    protected string SectionItem { get; }

//    protected INode? Find(string existing)
//        => Section switch {
//            nameof(Node.Traits) when Target is Node element => element.Traits.FirstOrDefault(i => i.Name == existing),
//            nameof(Domain.PowerSources) when Target is Domain ruleSet => ruleSet.PowerSources.FirstOrDefault(i => i.Name == existing),
//            _ => throw new NotImplementedException()
//        };

//    protected void Add(INode item) {
//        switch (Target) {
//            case Node element when item is Trait trait:
//                element.Traits.Add(trait);
//                return;
//            case Domain ruleSet when item is PowerSource powerSource:
//                ruleSet.PowerSources.Add(powerSource);
//                return;
//            default:
//                throw new NotImplementedException();
//        }
//    }

//    protected void Remove(INode item) {
//        switch (Section) {
//            case nameof(Node.Traits) when Target is Node element:
//                element.Traits.Remove((ITrait)item);
//                return;
//            case nameof(Domain.PowerSources) when Target is Domain ruleSet:
//                ruleSet.PowerSources.Remove((IPowerSource)item);
//                return;
//            default:
//                throw new NotImplementedException();
//        }
//    }

//    private class MainCommands : SectionBuilder, ISectionBuilder.IMainCommands {
//        public MainCommands(IPersisted entity, string section) : base(entity, section) {
//        }

//        public ISectionBuilder.IConnector Add(string name, string description, Action<IEntityUpdater.IMain> configure)
//            => Add(name, description, (_, x) => configure(x));

//        public ISectionBuilder.IConnector Add(string name, string description, Action<IPersisted, IEntityUpdater.IMain> configure) {
//            var factory = ComponentFactory.For(Target, Target.Owner);
//            var item = factory.Create(SectionItem, name, description);
//            configure(Target, EntityUpdater.For(item));
//            Add(item);
//            return new Connector(Target, Section);
//        }

//        public ISectionBuilder.IConnector Remove(string existing) {
//            var item = Find(existing) ?? throw new InvalidOperationException($"{SectionItem} named {existing} not found.");
//            Remove(item);
//            return new Connector(Target, Section);
//        }

//        public ISectionBuilder.IReplaceWith Replace(string existing) {
//            Remove(existing);
//            return new ReplaceWith(Target, Section);
//        }

//        public ISectionBuilder.IAppendWith Append(string existing) {
//            var item = Find(existing) ?? throw new InvalidOperationException($"{SectionItem} named {existing} not found.");
//            return new AppendWith(Target, Section, item);
//        }
//    }

//    private class Connector : SectionBuilder, ISectionBuilder.IConnector {
//        public Connector(IPersisted entity, string section) : base(entity, section) {
//        }

//        public ISectionBuilder.IMainCommands And() => new MainCommands(Target, Section);
//    }

//    private class ReplaceWith : SectionBuilder, ISectionBuilder.IReplaceWith {
//        public ReplaceWith(IPersisted entity, string section) : base(entity, section) {
//        }

//        public ISectionBuilder.IConnector With(string name, string description, Action<IEntityUpdater.IMain> configure) {
//            var mainCommands = new MainCommands(Target, Section);
//            return mainCommands.Add(name, description, (_, x) => configure(x));
//        }

//        public ISectionBuilder.IConnector With(string name, string description, Action<IPersisted, IEntityUpdater.IMain> configure) {
//            var mainCommands = new MainCommands(Target, Section);
//            return mainCommands.Add(name, description, configure);
//        }
//    }

//    private class AppendWith : SectionBuilder, ISectionBuilder.IAppendWith {
//        private INode _original;

//        public AppendWith(IPersisted entity, string section, INode original) : base(entity, section) {
//            _original = original;
//        }

//        public ISectionBuilder.IConnector With(string additionalDescription, Action<IEntityUpdater.IMain> configure)
//            => With(additionalDescription, (_, e) => configure(e));

//        public ISectionBuilder.IConnector With(string additionalDescription, Action<IPersisted, IEntityUpdater.IMain> configure) {
//            _original = (Node)_original with {
//                Description = $"{_original.Description}\n{additionalDescription}",
//            };
//            return With(configure);
//        }

//        public ISectionBuilder.IConnector With(Action<IEntityUpdater.IMain> configure)
//                => With((_, x) => configure(x));

//        public ISectionBuilder.IConnector With(Action<IPersisted, IEntityUpdater.IMain> configure) {
//            configure(Target, EntityUpdater.For(_original));
//            Add(_original);
//            return new Connector(Target, Section);
//        }
//    }
//}
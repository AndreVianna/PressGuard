//namespace RolePlayReady.Utilities.Contracts;

//public interface ISectionBuilder {
//    public interface IMainCommands {
//        IConnector Add(string name, string description, Action<IEntityUpdater.IMain> configure);
//        IConnector Add(string name, string description, Action<IPersisted, IEntityUpdater.IMain> configure);

//        IConnector Remove(string existing);
//        IReplaceWith Replace(string existing);
//        IAppendWith Append(string existing);
//    }

//    public interface IConnector {
//        IMainCommands And();
//    }

//    public interface IReplaceWith {
//        IConnector With(string name, string description, Action<IEntityUpdater.IMain> configure);
//        IConnector With(string name, string description, Action<IPersisted, IEntityUpdater.IMain> configure);
//    }

//    public interface IAppendWith {
//        IConnector With(string additionalDescription, Action<IEntityUpdater.IMain> configure);
//        IConnector With(string additionalDescription, Action<IPersisted, IEntityUpdater.IMain> configure);
//        IConnector With(Action<IEntityUpdater.IMain> configure);
//        IConnector With(Action<IPersisted, IEntityUpdater.IMain> configure);
//    }
//}

namespace RemoteService.Models;

public interface IPersisted : IEntity {
    DateTime ChangeStamp { get; init; }
}
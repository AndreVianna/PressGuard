namespace RemoteService.Models.Abstractions;

public interface IPersisted : IEntity {
    DateTime ChangeStamp { get; init; }
}
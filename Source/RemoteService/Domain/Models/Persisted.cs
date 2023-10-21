using RemoteService.Models.Abstractions;

namespace RemoteService.Models;

public abstract record Persisted : Base, IPersisted {
    public Guid Id { get; init; } = Guid.NewGuid();

    public State State { get; init; }

    public DateTime ChangeStamp { get; init; }
}

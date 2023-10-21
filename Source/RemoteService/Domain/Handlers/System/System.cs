namespace RemoteService.Handlers.System;

public record System : PersistedBase {
    public ICollection<PersistedBase> Domains { get; init; } = new List<PersistedBase>();
}

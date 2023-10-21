namespace RemoteService.Models;

public abstract record Component : PersistedBase {
    public ICollection<IAttribute> Attributes { get; init; } = new List<IAttribute>();
}

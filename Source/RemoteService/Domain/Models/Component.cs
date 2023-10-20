using RemoteService.Models.Abstractions;

namespace RemoteService.Models;

public abstract record Component : Persisted {
    public ICollection<IAttribute> Attributes { get; init; } = new List<IAttribute>();
}
using RemoteService.Models;

namespace RemoteService.Handlers.System;

public record System : Persisted {
    public ICollection<Base> Domains { get; init; } = new List<Base>();
}

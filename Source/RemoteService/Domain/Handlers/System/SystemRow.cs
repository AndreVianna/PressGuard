using RemoteService.Models;

namespace RemoteService.Handlers.System;

public record SystemRow : Row {
    public required string Name { get; init; }
}

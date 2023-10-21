namespace RemoteService.Models;

public record GameObject : Component, IGameObject {
    public required string Unit { get; init; }
}
namespace RemoteService.Models;

public record Row : IEntity {
    public required Guid Id { get; init; }
}
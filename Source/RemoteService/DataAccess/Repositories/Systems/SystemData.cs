namespace DataAccess.Repositories.Systems;

public record SystemData : IPersisted  {
    public Guid Id { get; init; }
    public State State { get; init; }
    public string? ShortName { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string[] Tags { get; init; }

    public DateTime ChangeStamp { get; init; }
}

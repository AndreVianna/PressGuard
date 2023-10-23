namespace RemoteService.Handlers.Venues;

public record VenueRow : Row {
    public required string Name { get; init; }
}

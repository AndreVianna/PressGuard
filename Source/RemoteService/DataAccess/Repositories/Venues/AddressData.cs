namespace RemoteService.Repositories.Venues;

public record AddressData{
    public required string Line1 { get; init; }
    public string? Line2 { get; init; }
    public string? City { get; init; }
    public string? Province { get; init; }
    public required string ZipCode { get; init; }
}

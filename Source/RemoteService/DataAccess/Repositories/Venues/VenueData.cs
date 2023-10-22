namespace RemoteService.Repositories.Venues;

public record VenueData : PersistedBase {
    public required AddressData Address { get; init; }
    public DeviceData[] Devices { get; init; } = Array.Empty<DeviceData>();
}

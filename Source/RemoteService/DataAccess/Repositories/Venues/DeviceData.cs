namespace RemoteService.Repositories.Venues;

public record DeviceData : Base {
    public required int Port { get; init; }
    public ICollection<SensorData> Sensors { get; init; } = new List<SensorData>();
}

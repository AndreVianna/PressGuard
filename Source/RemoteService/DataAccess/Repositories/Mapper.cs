using RemoteService.Handlers.Tenants;
using RemoteService.Handlers.Venues;
using RemoteService.Repositories.Tenants;
using RemoteService.Repositories.Venues;

namespace RemoteService.Repositories;

public static class Mapper {
    public static TenantData ToData(Tenant input)
        => new() {
            Id = input.Id,
            State = input.State,
            Name = input.Name,
            Description = input.Description,
            Venues = input.Venues.ToArray(i => i.ToData()),
            ChangeStamp = input.ChangeStamp,
        };

    public static TenantRow ToRow(TenantData input)
        => new() {
            Id = input.Id,
            Name = input.Name,
        };

    public static Tenant? ToModel(TenantData? input)
        => input is null
            ? null
            : new() {
                Id = input.Id,
                State = input.State,
                Name = input.Name,
                Description = input.Description,
                Venues = input.Venues.ToArray(i => i.ToModel()!),
                ChangeStamp = input.ChangeStamp,
            };

    public static VenueData ToData(this Venue input)
        => new() {
            Id = input.Id,
            State = input.State,
            Address = input.Address.ToData(),
            Name = input.Name,
            Description = input.Description,
            Devices = input.Devices.ToArray(i => i.ToData()),
            ChangeStamp = input.ChangeStamp,
        };

    public static VenueRow ToRow(this VenueData input)
        => new() {
            Id = input.Id,
            Name = input.Name,
        };

    public static Venue? ToModel(this VenueData? input)
        => input is null
            ? null
            : new() {
                Id = input.Id,
                State = input.State,
                Address = input.Address.ToModel(),
                Name = input.Name,
                Description = input.Description,
                Devices = input.Devices.ToArray(i => i.ToModel()),
                ChangeStamp = input.ChangeStamp,
            };

    private static AddressData ToData(this Address input)
        => new() {
            Line1 = input.Line1,
            Line2 = input.Line2,
            City = input.City,
            Province = input.Province,
            ZipCode = input.ZipCode,
        };

    private static Address ToModel(this AddressData input)
        => new() {
            Line1 = input.Line1,
            Line2 = input.Line2,
            City = input.City,
            Province = input.Province,
            ZipCode = input.ZipCode,
        };

    public static DeviceData ToData(this Device input)
        => new() {
            Port = input.Port,
            Name = input.Name,
            Description = input.Description,
            Sensors = input.Sensors.ToArray(i => i.ToData()),
        };

    public static Device ToModel(this DeviceData input)
        => new() {
            Port = input.Port,
            Name = input.Name,
            Description = input.Description,
            Sensors = input.Sensors.ToArray(i => i.ToModel()),
        };

    public static SensorData ToData(this Sensor input)
        => new() {
            Model = input.Model,
        };

    public static Sensor ToModel(this SensorData input)
        => new() {
            Model = input.Model,
        };
}

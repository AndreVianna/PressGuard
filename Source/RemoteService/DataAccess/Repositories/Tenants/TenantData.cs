using RemoteService.Repositories.Venues;

namespace RemoteService.Repositories.Tenants;

public record TenantData : PersistedBase  {
    public required VenueData[] Venues { get; init; }
}

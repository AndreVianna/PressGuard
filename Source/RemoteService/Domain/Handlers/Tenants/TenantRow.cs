namespace RemoteService.Handlers.Tenants;

public record TenantRow : Row {
    public required string Name { get; init; }
}

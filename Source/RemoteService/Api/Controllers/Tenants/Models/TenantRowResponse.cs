namespace RemoteService.Controllers.Tenants.Models;

[SwaggerSchema("The model that identifies a game system in a list.", ReadOnly = true)]
public record TenantRowResponse {
    [SwaggerSchema("The id of the game system.", ReadOnly = true)]
    public required string Id { get; init; }

    [SwaggerSchema("The name of the game system.", ReadOnly = true)]
    public required string Name { get; init; }
}

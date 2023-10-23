namespace RemoteService.Controllers.Tenants.Models;

[SwaggerSchema("The model that represents a game system.", ReadOnly = true)]
public record TenantResponse {
    [SwaggerSchema("The id of the game system.", ReadOnly = true)]
    public required string Id { get; init; }

    [SwaggerSchema("The name of the game system.", ReadOnly = true)]
    public required string Name { get; init; }

    [SwaggerSchema("The description of the game system.", ReadOnly = true)]
    public required string Description { get; init; }

    [SwaggerSchema("The optional short name of the game system.", ReadOnly = true)]
    public string? ShortName { get; init; }

    [SwaggerSchema("A collection of tags used to qualify the game system.", ReadOnly = true)]
    public ICollection<string> Tags { get; init; } = Array.Empty<string>();
}

namespace RemoteService.Controllers.Users.Models;

[SwaggerSchema("The model that identifies a user in a list.", ReadOnly = true)]
public record UserRowResponse {
    [SwaggerSchema("The id of the user.", ReadOnly = true)]
    public required string Id { get; init; }

    [SwaggerSchema("The email of the user.", ReadOnly = true)]
    public required string Email { get; init; }

    [SwaggerSchema("The name of the user.", ReadOnly = true)]
    public required string Name { get; init; }
}
namespace RemoteService.Controllers.Users.Models;

[SwaggerSchema("The model that represents a user.", ReadOnly = true)]
public record UserResponse {
    [SwaggerSchema("The id of the user.", ReadOnly = true)]
    public required string Id { get; init; }

    [SwaggerSchema("The email of the user.", ReadOnly = true)]
    public required string Email { get; init; }

    [SwaggerSchema("The indicates if the user is temporarily locked.", ReadOnly = true)]
    public bool IsLocked { get; init; }

    [SwaggerSchema("The indicates if the user is permanently blocked.", ReadOnly = true)]
    public bool IsBlocked { get; init; }

    [SwaggerSchema("The indicates the list of roles assigned to the user.", ReadOnly = true)]
    public string[] Roles { get; init; } = Array.Empty<string>();

    [SwaggerSchema("The first name of the user.", ReadOnly = true)]
    public string? FirstName { get; init; }

    [SwaggerSchema("The last name of the user.", ReadOnly = true)]
    public string? LastName { get; init; }

    [SwaggerSchema("The name of the user.", ReadOnly = true)]
    public DateOnly? Birthday { get; init; }
}
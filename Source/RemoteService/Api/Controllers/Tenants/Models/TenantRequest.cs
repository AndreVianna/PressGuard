using RemoteService.Constants;

namespace RemoteService.Controllers.Tenants.Models;

[SwaggerSchema("The request model used to create or update a game system.")]
public record TenantRequest {
    [Required]
    [MaxLength(Validation.Name.MaximumLength)]
    [MinLength(Validation.Name.MinimumLength)]
    [SwaggerSchema("The name of the game system.")]
    public required string Name { get; init; }

    [Required]
    [MaxLength(Validation.Description.MaximumLength)]
    [MinLength(Validation.Description.MinimumLength)]
    [SwaggerSchema("The description of the game system.")]
    public required string Description { get; init; }
}

using RemoteService.Constants;

namespace RemoteService.Controllers.Systems.Models;

[SwaggerSchema("The request model used to create or update a game system.")]
public record SystemRequest {
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

    [MinLength(Validation.ShortName.MinimumLength)]
    [MaxLength(Validation.ShortName.MaximumLength)]
    [SwaggerSchema("The optional short name of the game system.")]
    public string? ShortName { get; init; }

    [SwaggerSchema("A collection of tags used to qualify the game system.")]
    public ICollection<string> Tags { get; init; } = Array.Empty<string>();
}

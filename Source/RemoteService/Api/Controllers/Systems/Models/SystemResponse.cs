using RemoteService.Constants;

namespace RemoteService.Controllers.Systems.Models;

[SwaggerSchema("The model that represents a game system.", ReadOnly = true)]
public record SystemResponse {
    [Required]
    [SwaggerSchema("The id of the game system.", ReadOnly = true)]
    public required string Id { get; init; }

    [Required]
    [MaxLength(Validation.Name.MaximumLength)]
    [MinLength(Validation.Name.MinimumLength)]
    [SwaggerSchema("The name of the game system.", ReadOnly = true)]
    public required string Name { get; init; }

    [Required]
    [MaxLength(Validation.Description.MaximumLength)]
    [MinLength(Validation.Description.MinimumLength)]
    [SwaggerSchema("The description of the game system.", ReadOnly = true)]
    public required string Description { get; init; }

    [MinLength(Validation.ShortName.MinimumLength)]
    [MaxLength(Validation.ShortName.MaximumLength)]
    [SwaggerSchema("The optional short name of the game system.", ReadOnly = true)]
    public string? ShortName { get; init; }

    [SwaggerSchema("A collection of tags used to qualify the game system.", ReadOnly = true)]
    public ICollection<string> Tags { get; init; } = Array.Empty<string>();
}
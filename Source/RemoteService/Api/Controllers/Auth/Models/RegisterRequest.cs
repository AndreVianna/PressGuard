using RemoteService.Constants;

using DataType = System.ComponentModel.DataAnnotations.DataType;

namespace RemoteService.Controllers.Auth.Models;

public record RegisterRequest
{
    [Required]
    [EmailAddress]
    [SwaggerSchema("The email of the user.")]
    public required string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [SwaggerSchema("The password of the user.", Format = "password")]
    public required string Password { get; set; }

    [MinLength(Validation.Name.MinimumLength)]
    [MaxLength(Validation.Name.MaximumLength)]
    [SwaggerSchema("The name of the user.")]
    public string? FirstName { get; init; }

    [MinLength(Validation.Name.MinimumLength)]
    [MaxLength(Validation.Name.MaximumLength)]
    [SwaggerSchema("The first name (given name) of the user.")]
    public string? LastName { get; init; }
}

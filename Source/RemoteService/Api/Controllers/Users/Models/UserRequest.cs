using RemoteService.Constants;

using DataType = System.ComponentModel.DataAnnotations.DataType;

namespace RemoteService.Controllers.Users.Models;

[SwaggerSchema("The request model used to create or update a user.")]
public record UserRequest {
    [Required]
    [EmailAddress]
    [SwaggerSchema("The email of the user.", ReadOnly = true)]
    public required string Email { get; init; }

    [MinLength(Validation.Name.MinimumLength)]
    [MaxLength(Validation.Name.MaximumLength)]
    [SwaggerSchema("The name of the user.", ReadOnly = true)]
    public string? FirstName { get; init; }

    [MinLength(Validation.Name.MinimumLength)]
    [MaxLength(Validation.Name.MaximumLength)]
    [SwaggerSchema("The first name (given name) of the user.", ReadOnly = true)]
    public string? LastName { get; init; }

    [DataType(DataType.Date)]
    [SwaggerSchema("The last name (family name) of the user.", ReadOnly = true)]
    public DateOnly? Birthday { get; init; }
}

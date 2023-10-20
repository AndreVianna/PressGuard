using DataType = System.ComponentModel.DataAnnotations.DataType;

namespace RemoteService.Controllers.Auth.Models;

public record LoginRequest {
    [Required]
    [EmailAddress]
    [SwaggerSchema("The email of the user.")]
    public required string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [SwaggerSchema("The password of the user.", Format = "password")]
    public required string Password { get; set; }
}


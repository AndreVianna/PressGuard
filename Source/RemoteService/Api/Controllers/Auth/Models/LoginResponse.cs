namespace RemoteService.Controllers.Auth.Models;

public record LoginResponse {
    public required string Token { get; set; }
}
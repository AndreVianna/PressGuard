namespace RemoteService.Handlers.Auth;

public record AuthSettings {
    public required bool Requires2Factor { get; set; }
    public required string IssuerSigningKey { get; set; }
    public required int SignInTokenExpirationInHours { get; set; }
    public required int EmailTokenExpirationInMinutes { get; set; }
}

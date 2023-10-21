namespace RemoteService.Handlers.Auth;

public class TokenGenerator : ITokenGenerator {
    private readonly DateTimeProvider _dateTime;
    private readonly AuthSettings _authSettings;

    public TokenGenerator(IOptions<AuthSettings> authSettings, DateTimeProvider dateTime) {
        _dateTime = dateTime;
        _authSettings = authSettings.Value;
    }

    public string GenerateSignInToken(User user) {
        var claims = GenerateSignInClaims(user);
        var expiration = _dateTime.Now.AddHours(_authSettings.SignInTokenExpirationInHours);
        return GenerateToken(claims, expiration);
    }

    public string GenerateEmailConfirmationToken(User user) {
        var claims = new List<Claim> {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
        };
        var expiration = _dateTime.Now.AddMinutes(_authSettings.EmailTokenExpirationInMinutes);
        return GenerateToken(claims, expiration);
    }

    private static IEnumerable<Claim> GenerateSignInClaims(User user) {
        var claims = new List<Claim> {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
        };
        if (user.FirstName is not null)
            claims.Add(new Claim(ClaimTypes.GivenName, user.FirstName));
        claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role.ToString())));
        return claims;
    }

    private string GenerateToken(IEnumerable<Claim> claims, DateTime expiration) {
        var credentials = GetCredentials();

        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(claims),
            Expires = expiration,
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private SigningCredentials GetCredentials() {
        var issuerSigningKey = Ensure.IsNotNullOrWhiteSpace(_authSettings.IssuerSigningKey);
        var key = new SymmetricSecurityKey(Base64UrlEncoder.DecodeBytes(issuerSigningKey));
        return new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    }
}

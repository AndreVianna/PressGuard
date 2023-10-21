using System.Text;

namespace RemoteService.Authentication;

public class TokenGenerator : ITokenGenerator {
    private readonly DateTimeProvider _dateTime;
    private readonly AuthSettings _authSettings;

    public TokenGenerator(IOptions<AuthSettings> authSettings, DateTimeProvider dateTime) {
        _dateTime = dateTime;
        _authSettings = authSettings.Value;
    }

    public string GenerateSignInToken(User user) {
        var claims = GenerateSignInClaims(user);
        var expiration = TimeSpan.FromHours(_authSettings.SignInTokenExpirationInHours);
        return GenerateToken(claims, expiration);
    }

    public string GenerateEmailConfirmationToken(User user) {
        var claims = new List<Claim> {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
        };
        var expiration = TimeSpan.FromMinutes(_authSettings.EmailTokenExpirationInMinutes);
        return GenerateToken(claims, expiration);
    }

    private static IEnumerable<Claim> GenerateSignInClaims(User user) {
        var claims = new List<Claim> {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
        };
        if (user.FirstName is not null)
            claims.Add(new(ClaimTypes.GivenName, user.FirstName));
        claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role.ToString())));
        return claims;
    }

    private string GenerateToken(IEnumerable<Claim> claims, TimeSpan duration) {
        var credentials = GetCredentials();

        var now = _dateTime.UtcNow;
        var expiration = now + duration;
        var token = new JwtSecurityToken(null,
                                         null,
                                         claims,
                                         now,
                                         expiration,
                                         credentials);
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }

    private SigningCredentials GetCredentials() {
        var issuerSigningKey = Ensure.IsNotNullOrWhiteSpace(_authSettings.IssuerSigningKey);
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(issuerSigningKey));
        return new(key, SecurityAlgorithms.HmacSha256);
    }
}

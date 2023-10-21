namespace RemoteService.Authentication; 

public class TokenGeneratorTests {
    private readonly DateTimeProvider _dateTimeProvider;
    private readonly AuthSettings _authSettings;
    private readonly TokenGenerator _tokenGenerator;

    public TokenGeneratorTests() {
        _dateTimeProvider = Substitute.For<DateTimeProvider>();
        _authSettings = new AuthSettings {
            IssuerSigningKey = "ASecretValueWith256BitsOr32Chars",
            SignInTokenExpirationInHours = 1,
            EmailTokenExpirationInMinutes = 30,
            Requires2Factor = false,
        };
        var authSettingsOptions = Options.Create(_authSettings);
        _tokenGenerator = new TokenGenerator(authSettingsOptions, _dateTimeProvider);
    }

    [Fact]
    public void GenerateSignInToken_Should_Return_Token_With_Correct_Claims_And_Expiration() {
        // Arrange
        var user = new User {
            Id = Guid.NewGuid(),
            Email = "test@test.com",
            FirstName = "Test",
            Roles = new List<Role> { Role.Administrator, Role.User }
        };
        var now = DateTime.UtcNow.AddMinutes(1);
        var expectedExpiration = now.AddHours(_authSettings.SignInTokenExpirationInHours);
        _dateTimeProvider.UtcNow.Returns(now);

        // Act
        var token = _tokenGenerator.GenerateSignInToken(user);
        var tokenHandler = new JwtSecurityTokenHandler();
        var decodedToken = tokenHandler.ReadJwtToken(token);

        // Assert
        decodedToken.Claims.Should().Contain(c => (c.Type == ClaimTypes.NameIdentifier) && (c.Value == user.Id.ToString()));
        decodedToken.Claims.Should().Contain(c => (c.Type == ClaimTypes.Email) && (c.Value == user.Email));
        decodedToken.Claims.Should().Contain(c => (c.Type == ClaimTypes.GivenName) && (c.Value == user.FirstName));
        decodedToken.Claims.Should().Contain(c => (c.Type == ClaimTypes.Role) && (c.Value == Role.Administrator.ToString()));
        decodedToken.Claims.Should().Contain(c => (c.Type == ClaimTypes.Role) && (c.Value == Role.User.ToString()));
        decodedToken.ValidTo.Should().BeCloseTo(expectedExpiration, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void GenerateEmailConfirmationToken_Should_Return_Token_With_Correct_Claims_And_Expiration() {
        // Arrange
        var user = new User {
            Id = Guid.NewGuid(),
            Email = "test@test.com"
        };
        var now = DateTime.UtcNow.AddMinutes(1);
        var expectedExpiration = now.AddMinutes(_authSettings.EmailTokenExpirationInMinutes);
        _dateTimeProvider.UtcNow.Returns(now);

        // Act
        var token = _tokenGenerator.GenerateEmailConfirmationToken(user);
        var tokenHandler = new JwtSecurityTokenHandler();
        var decodedToken = tokenHandler.ReadJwtToken(token);

        // Assert
        decodedToken.Claims.Should().Contain(c => (c.Type == ClaimTypes.NameIdentifier) && (c.Value == user.Id.ToString()));
        decodedToken.ValidTo.Should().BeCloseTo(expectedExpiration, TimeSpan.FromSeconds(1));
    }
}

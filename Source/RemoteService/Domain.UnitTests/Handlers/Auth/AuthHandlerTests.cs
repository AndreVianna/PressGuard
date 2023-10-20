using Microsoft.Extensions.Options;

using RemoteService.Repositories.User;

namespace RemoteService.Handlers.Auth;

public class AuthHandlerTests {
    private readonly AuthHandler _handler;
    private static readonly string _validEmail = "some.user@host.com";
    private static readonly string _unconfirmedEmail = "unconfirmed@host.com";
    private static readonly string _validPassword = "Secret1234!";
    private static readonly string _invalidPassword = "Invalid";
    private static readonly ITokenGenerator _tokenGenerator = Substitute.For<ITokenGenerator>();
    private static readonly IEmailSender _emailSender = Substitute.For<IEmailSender>();
    private readonly IUserRepository _repository;
    private static readonly IHasher _hasher = Substitute.For<IHasher>();
    private static readonly HashedSecret _validSecret = new("Secret1234!"u8.ToArray(), "Secret1234!"u8.ToArray());

    public AuthHandlerTests() {
        _hasher.HashSecret(Arg.Any<string>(), Arg.Any<byte[]>()).Returns(_validSecret);

        _repository = Substitute.For<IUserRepository>();
        var validUser = new User {
            Id = Guid.NewGuid(),
            Email = _validEmail,
            HashedPassword = _validSecret,
            FirstName = "Some",
            LastName = "User",
            IsConfirmed = true,
        };
        _repository.VerifyAsync(Arg.Is<SignIn>(i => i.Email == _validEmail && i.Password == _validPassword), Arg.Any<CancellationToken>()).Returns(validUser);

        var unconfirmedUser = new User {
            Id = Guid.NewGuid(),
            Email = _unconfirmedEmail,
            HashedPassword = _validSecret,
            FirstName = "Unconfirmed",
            LastName = "User",
            IsConfirmed = false,
        };
        _repository.VerifyAsync(Arg.Is<SignIn>(i => i.Email == _unconfirmedEmail && i.Password == _validPassword), Arg.Any<CancellationToken>()).Returns(unconfirmedUser);

        var authSettings = new AuthSettings {
            Requires2Factor = false,
            IssuerSigningKey = "12345678901234567890123456789012",
            SignInTokenExpirationInHours = 7,
            EmailTokenExpirationInMinutes = 7,
        };
        var options = Substitute.For<IOptions<AuthSettings>>();
        options.Value.Returns(authSettings);

        _handler = new AuthHandler(_repository, _hasher, options, _emailSender, _tokenGenerator, NullLogger<AuthHandler>.Instance);
    }

    private class TestLoginData : TheoryData<SignIn, bool, string[]> {
        public TestLoginData() {
            Add(new() { Email = _validEmail, Password = _validPassword }, true, Array.Empty<string>());
            Add(new() { Email = _unconfirmedEmail, Password = _validPassword }, false, Array.Empty<string>());
            Add(new() { Email = _validEmail, Password = _invalidPassword }, false, Array.Empty<string>());
            Add(new() { Email = "invalid.user@email.com", Password = _validPassword }, false, Array.Empty<string>());
            Add(new() { Email = null!, Password = null! }, false, new[] { "'Email' cannot be null.", "'Password' cannot be null." });
        }
    }

    [Theory]
    [ClassData(typeof(TestLoginData))]
    public async Task SignInAsync_AuthenticatesUserLogin(SignIn signIn, bool isSuccess, string[] errors) {
        // Act
        var result = await _handler.SignInAsync(signIn);

        // Assert
        result.IsSuccess.Should().Be(isSuccess);
        result.Errors.Select(i => i.Message).Should().BeEquivalentTo(errors);
    }

    [Fact]
    public async Task SignInAsync_WithTwoFactor_ReturnsRequiresTwoFactor() {
        // Arrange
        var authSettings = new AuthSettings {
            Requires2Factor = true,
            IssuerSigningKey = "12345678901234567890123456789012",
            SignInTokenExpirationInHours = 7,
            EmailTokenExpirationInMinutes = 7,
        };
        var options = Substitute.For<IOptions<AuthSettings>>();
        options.Value.Returns(authSettings);

        var handler = new AuthHandler(_repository, _hasher, options, _emailSender, _tokenGenerator, NullLogger<AuthHandler>.Instance);
        var signIn = new SignIn { Email = _validEmail, Password = _validPassword };

        // Act
        var result = await handler.SignInAsync(signIn);

        // Assert
        result.IsSuccess.Should().Be(false);
        result.RequiresTwoFactor.Should().Be(true);
        result.Errors.Should().HaveCount(0);
    }

    private class TestRegisterData : TheoryData<SignOn, bool, string[]> {
        public TestRegisterData() {
            Add(new() { Email = _validEmail, Password = _validPassword, FirstName = "Some", LastName = "Name" }, true, Array.Empty<string>());
            Add(new() { Email = "duplicated.user@email.com", Password = _validPassword }, false, Array.Empty<string>());
            Add(new() { Email = null!, Password = null! }, false, new[] { "'Email' cannot be null.", "'Password' cannot be null." });
        }
    }

    [Theory]
    [ClassData(typeof(TestRegisterData))]
    public async Task Register_AddsUser(SignOn signOn, bool isSuccess, string[] errors) {
        // Arrange
        var input = CreateInput();
        _repository.AddAsync(Arg.Any<User>(), Arg.Any<CancellationToken>()).Returns(input);
        _repository.AddAsync(Arg.Is<User>(i => i.Email == "duplicated.user@email.com"), Arg.Any<CancellationToken>()).Returns(default(User?));

        // Act
        var result = await _handler.RegisterAsync(signOn);

        // Assert
        result.IsSuccess.Should().Be(isSuccess);
        result.Errors.Select(i => i.Message).Should().BeEquivalentTo(errors);
    }

    [Fact]
    public async Task GetManyAsync_ReturnsUsers() {
        // Arrange
        var expected = new[] { CreateRow() };
        _repository.GetManyAsync(Arg.Any<CancellationToken>()).Returns(expected);

        // Act
        var result = await _handler.GetManyAsync();

        // Assert
        result.IsInvalid.Should().BeFalse();
        result.Value.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsUser() {
        // Arrange
        var id = Guid.NewGuid();
        var expected = CreateInput(id);
        _repository.GetByIdAsync(id, Arg.Any<CancellationToken>()).Returns(expected);

        // Act
        var result = await _handler.GetByIdAsync(id);

        // Assert
        result.WasNotFound.Should().BeFalse();
        result.IsInvalid.Should().BeFalse();
        result.Value.Should().Be(expected);
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ReturnsNotFound() {
        // Arrange
        var id = Guid.NewGuid();
        _repository.GetByIdAsync(id, Arg.Any<CancellationToken>()).Returns(default(User));

        // Act
        var result = await _handler.GetByIdAsync(id);

        // Assert
        result.WasNotFound.Should().BeTrue();
        result.IsInvalid.Should().BeFalse();
        result.Value.Should().BeNull();
    }

    [Fact]
    public async Task AddAsync_ReturnsUser() {
        // Arrange
        var input = CreateInput();
        _repository.AddAsync(input, Arg.Any<CancellationToken>()).Returns(input);

        // Act
        var result = await _handler.AddAsync(input);

        // Assert
        result.IsInvalid.Should().BeFalse();
        result.HasConflict.Should().BeFalse();
        result.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task AddAsync_ForExistingId_ReturnsConflict() {
        // Arrange
        var input = CreateInput();
        _repository.AddAsync(input, Arg.Any<CancellationToken>()).Returns(default(User));

        // Act
        var result = await _handler.AddAsync(input);

        // Assert
        result.IsInvalid.Should().BeFalse();
        result.HasConflict.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task AddAsync_WithErrors_ReturnsFailure() {
        // Arrange
        var input = new User {
            Id = Guid.NewGuid(),
            Email = null!,
            HashedPassword = null,
        };

        // Act
        var result = await _handler.AddAsync(input);

        // Assert
        result.IsInvalid.Should().BeTrue();
        result.HasConflict.Should().BeFalse();
        result.Value.Should().Be(input);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsUser() {
        // Arrange
        var id = Guid.NewGuid();
        var input = CreateInput(id);
        _repository.GetByIdAsync(id, Arg.Any<CancellationToken>()).Returns(input);
        _repository.UpdateAsync(input, Arg.Any<CancellationToken>()).Returns(input);

        // Act
        var result = await _handler.UpdateAsync(input);

        // Assert
        result.IsInvalid.Should().BeFalse();
        result.WasNotFound.Should().BeFalse();
        result.Value.Should().Be(input);
    }

    [Fact]
    public async Task UpdateAsync_WithInvalidId_ReturnsNotFound() {
        // Arrange
        var id = Guid.NewGuid();
        var input = CreateInput(id);
        _repository.UpdateAsync(input, Arg.Any<CancellationToken>()).Returns(default(User));

        // Act
        var result = await _handler.UpdateAsync(input);

        // Assert
        result.IsInvalid.Should().BeFalse();
        result.WasNotFound.Should().BeTrue();
        result.Value.Should().BeNull();
    }

    [Fact]
    public async Task UpdateAsync_WithErrors_ReturnsFailure() {
        // Arrange
        var input = new User {
            Id = Guid.NewGuid(),
            Email = null!,
        };

        // Act
        var result = await _handler.UpdateAsync(input);

        // Assert
        result.IsInvalid.Should().BeTrue();
        result.WasNotFound.Should().BeFalse();
        result.Value.Should().Be(input);
    }

    [Fact]
    public async Task Remove_ReturnsTrue() {
        // Arrange
        var id = Guid.NewGuid();
        var input = CreateInput(id);
        _repository.GetByIdAsync(id, Arg.Any<CancellationToken>()).Returns(input);
        _repository.RemoveAsync(id, Arg.Any<CancellationToken>()).Returns(true);

        // Act
        var result = await _handler.RemoveAsync(id);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Remove_WithInvalidId_ReturnsNotFound() {
        // Arrange
        var id = Guid.NewGuid();
        _repository.RemoveAsync(id, Arg.Any<CancellationToken>()).Returns(false);

        // Act
        var result = await _handler.RemoveAsync(id);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.WasNotFound.Should().BeTrue();
    }

    [Fact]
    public async Task GrantRoleAsync_WithValidUser_ReturnsTrue() {
        // Arrange
        var user = CreateInput();

        _repository.GetByIdAsync(user.Id, Arg.Any<CancellationToken>()).Returns(user);
        _repository.UpdateAsync(user, Arg.Any<CancellationToken>()).Returns(user);

        var input = new UserRole {
            UserId = user.Id,
            Role = Role.Administrator,
        };

        // Act
        var result = await _handler.GrantRoleAsync(input);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task GrantRoleAsync_WithInvalidUser_ReturnsFalse() {
        // Arrange
        var id = Guid.NewGuid();

        _repository.GetByIdAsync(id, Arg.Any<CancellationToken>()).Returns(default(User));

        var input = new UserRole {
            UserId = id,
            Role = Role.Administrator,
        };

        // Act
        var result = await _handler.GrantRoleAsync(input);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.WasNotFound.Should().BeTrue();
    }

    [Fact]
    public async Task RevokeRoleAsync_WithValidUser_ReturnsTrue() {
        // Arrange
        var user = CreateInput();

        _repository.GetByIdAsync(user.Id, Arg.Any<CancellationToken>()).Returns(user);
        _repository.UpdateAsync(user, Arg.Any<CancellationToken>()).Returns(user);

        var input = new UserRole {
            UserId = user.Id,
            Role = Role.Administrator,
        };

        // Act
        var result = await _handler.RevokeRoleAsync(input);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task RevokeRoleAsync_WithInvalidUser_ReturnsFalse() {
        // Arrange
        var id = Guid.NewGuid();

        _repository.GetByIdAsync(id, Arg.Any<CancellationToken>()).Returns(default(User));

        var input = new UserRole {
            UserId = id,
            Role = Role.Administrator,
        };

        // Act
        var result = await _handler.RevokeRoleAsync(input);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.WasNotFound.Should().BeTrue();
    }

    private static UserRow CreateRow(Guid? id = null)
        => new() {
            Id = id ?? Guid.NewGuid(),
            Name = "Some User",
            Email = "some.user@email.com",
        };

    private static User CreateInput(Guid? id = null)
        => new() {
            Id = id ?? Guid.NewGuid(),
            Email = "some.user@email.com",
            FirstName = "Some User",
            Birthday = DateOnly.FromDateTime(DateTime.Today.AddYears(-30)),
        };
}

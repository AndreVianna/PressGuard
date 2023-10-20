using RemoteService.Controllers.Auth;
using RemoteService.Controllers.Auth.Models;
using RemoteService.Handlers.Auth;

using SignInResult = System.Results.SignInResult;

namespace RemoteService.Controllers;

public class AuthControllerTests {
    private readonly IAuthHandler _handler = Substitute.For<IAuthHandler>();
    private static readonly ILogger<AuthController> _logger = Substitute.For<ILogger<AuthController>>();
    private static readonly SignIn _signInSample = new() {
        Email = "some.user@host.com",
        Password = "Password!1234",
    };
    private static readonly SignOn _signOnSample = new() {
        FirstName = "New",
        LastName = "User",
        Email = "new.user@emial.com",
        Password = "password",
    };
    private readonly AuthController _controller;

    public AuthControllerTests() {
        _controller = new AuthController(_handler, _logger);
    }

    [Fact]
    public async Task Login_WithValidLogin_ReturnsToken() {
        // Arrange
        var request = new LoginRequest {
            Email = _signInSample.Email,
            Password = _signInSample.Password,
        };
        const string token = "ValidToken";
        var expected = token.ToLoginResponse();
        _handler.SignInAsync(Arg.Any<SignIn>()).Returns(SignInResult.Success(token));

        // Act
        var response = await _controller.LoginAsync(request);

        // Assert
        var result = response.Should().BeOfType<OkObjectResult>().Subject;
        var content = result.Value.Should().BeOfType<LoginResponse>().Subject;
        content.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task Login_WithIncorrectLogin_ReturnsToken() {
        // Arrange
        var request = new LoginRequest {
            Email = _signInSample.Email,
            Password = _signInSample.Password,
        };
        _handler.SignInAsync(Arg.Any<SignIn>()).Returns(SignInResult.Failure());

        // Act
        var response = await _controller.LoginAsync(request);

        // Assert
        response.Should().BeOfType<UnauthorizedResult>();
    }

    [Fact]
    public async Task Login_WithInvalidRequest_ReturnsToken() {
        // Arrange
        var request = new LoginRequest {
            Email = "invalid",
            Password = "invalid",
        };
        _handler.SignInAsync(Arg.Any<SignIn>()).Returns(SignInResult.Invalid("Some validation error.", "login"));

        // Act
        var response = await _controller.LoginAsync(request);

        // Assert
        var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
        var error = result.Value.Should().BeOfType<SerializableError>().Subject;
        error.Should().HaveCount(1);
    }

    [Fact]
    public async Task Register_WithValidInput_ReturnsOk() {
        // Arrange
        var request = new RegisterRequest {
            FirstName = _signOnSample.FirstName,
            LastName = _signOnSample.LastName,
            Email = _signOnSample.Email,
            Password = _signOnSample.Password,
        };

        _handler.RegisterAsync(Arg.Any<SignOn>()).Returns(CrudResult.Success());

        // Act
        var response = await _controller.RegisterAsync(request);

        // Assert
        response.Should().BeOfType<OkResult>();
    }

    [Fact]
    public async Task Register_WithInvalidEmail_ReturnsBadRequest() {
        // Arrange
        var request = new RegisterRequest {
            FirstName = _signOnSample.FirstName,
            LastName = _signOnSample.LastName,
            Email = "invalid-email",
            Password = _signOnSample.Password,
        };

        _handler.RegisterAsync(Arg.Any<SignOn>()).Returns(CrudResult.Invalid("Invalid email", "email"));

        // Act
        var response = await _controller.RegisterAsync(request);

        // Assert
        var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
        var error = result.Value.Should().BeOfType<SerializableError>().Subject;
        error.Should().HaveCount(1);
    }

    [Fact]
    public async Task Register_WithInvalidPassword_ReturnsBadRequest() {
        // Arrange
        var request = new RegisterRequest {
            FirstName = _signOnSample.FirstName,
            LastName = _signOnSample.LastName,
            Email = _signOnSample.Email,
            Password = "short",
        };

        _handler.RegisterAsync(Arg.Any<SignOn>()).Returns(CrudResult.Invalid("Password length is invalid", "password"));

        // Act
        var response = await _controller.RegisterAsync(request);

        // Assert
        var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
        var error = result.Value.Should().BeOfType<SerializableError>().Subject;
        error.Should().HaveCount(1);
    }

    [Fact]
    public async Task Register_WithExistingEmail_ReturnsConflict() {
        //Arrange
        var request = new RegisterRequest {
            FirstName = _signOnSample.FirstName,
            LastName = _signOnSample.LastName,
            Email = _signInSample.Email, // Existing email 
            Password = _signOnSample.Password
        };

        _handler.RegisterAsync(Arg.Any<SignOn>()).Returns(CrudResult.Conflict());

        //Act 
        var response = await _controller.RegisterAsync(request);

        //Assert
        var result = response.Should().BeOfType<ConflictObjectResult>().Subject;
        var message = result.Value.Should().BeOfType<string>().Subject;
        message.Should().Be($"'{_signInSample.Email}' is already in use.");
    }

    [Fact]
    public async Task GrantRole_WithValidInput_ReturnsOk() {
        //Arrange
        var userId = Guid.NewGuid();
        var request = new RoleRequest { Role = Role.Administrator };

        _handler.GrantRoleAsync(Arg.Any<UserRole>()).Returns(CrudResult.Success());

        //Act
        var response = await _controller.GrantRoleAsync(userId, request);

        //Assert
        response.Should().BeOfType<OkResult>();
    }

    [Fact]
    public async Task GrantRole_WithInvalidUserId_ReturnsBadRequest() {
        //Arrange
        var userId = Guid.NewGuid();
        var request = new RoleRequest { Role = Role.Administrator };

        _handler.GrantRoleAsync(Arg.Any<UserRole>())
            .Returns(CrudResult.NotFound());

        //Act
        var response = await _controller.GrantRoleAsync(userId, request);

        //Assert
        response.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task RevokeRole_WithValidInput_ReturnsOk() {
        //Arrange
        var userId = Guid.NewGuid();
        var request = new RoleRequest { Role = Role.Administrator };

        _handler.RevokeRoleAsync(Arg.Any<UserRole>()).Returns(CrudResult.Success());

        //Act
        var response = await _controller.RevokeRoleAsync(userId, request);

        //Assert
        response.Should().BeOfType<OkResult>();
    }

    [Fact]
    public async Task RevokeRole_WithInvalidUserId_ReturnsBadRequest() {
        //Arrange
        var userId = Guid.NewGuid();
        var request = new RoleRequest { Role = Role.Administrator };

        _handler.RevokeRoleAsync(Arg.Any<UserRole>())
                .Returns(CrudResult.NotFound());

        //Act
        var response = await _controller.RevokeRoleAsync(userId, request);

        //Assert
        response.Should().BeOfType<NotFoundResult>();
    }
}
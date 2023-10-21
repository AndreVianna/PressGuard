using RemoteService.Authentication;
using RemoteService.Controllers.Users;
using RemoteService.Controllers.Users.Models;
using RemoteService.Handlers.Auth;

using static System.Results.CrudResult;

namespace RemoteService.Controllers;

public class UsersControllerTests {
    private readonly IAuthHandler _handler = Substitute.For<IAuthHandler>();
    private static readonly ILogger<UsersController> _logger = Substitute.For<ILogger<UsersController>>();
    private static readonly UserRow[] _rows = new[] {
        new UserRow {
            Id = Guid.NewGuid(),
            Name = "Some User",
            Email = "some.user@email.com",
        },
        new UserRow {
            Id = Guid.NewGuid(),
            Name = "Other User",
            Email = "other.user@email.com",
        }
    };
    private static readonly User _sample = new() {
        Id = Guid.NewGuid(),
        Email = "some.user@email.com",
        FirstName = "Some",
        LastName = "User",
        Birthday = DateOnly.FromDateTime(DateTime.Today.AddYears(-30)),
        Roles = new[] { Role.User },
    };

    private readonly UsersController _controller;

    public UsersControllerTests() {
        _controller = new(_handler, new(), _logger);
    }

    [Fact]
    public async Task GetMany_ReturnsArrayOfUserRowResponses() {
        // Arrange
        var expectedRows = _rows.ToResponse();
        _handler.GetManyAsync(Arg.Any<CancellationToken>()).Returns(Success(_rows.AsEnumerable()));

        // Act
        var response = await _controller.GetMany();

        // Assert
        var result = response.Should().BeOfType<OkObjectResult>().Subject;
        var content = result.Value.Should().BeOfType<UserRowResponse[]>().Subject;
        content.Should().BeEquivalentTo(expectedRows);
    }

    [Fact]
    public async Task GetById_WithValidId_ReturnsUserResponse() {
        // Arrange
        var expected = _sample.ToResponse(DateTime.Now);
        var base64Id = (Base64Guid)_sample.Id;
        _handler.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(Success(_sample));

        // Act
        var response = await _controller.GetById(base64Id);

        // Assert
        var result = response.Should().BeOfType<OkObjectResult>().Subject;
        var content = result.Value.Should().BeOfType<UserResponse>().Subject;
        content.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task GetById_WithInvalidId_ReturnsNotFound() {
        // Act
        var response = await _controller.GetById("invalid");

        // Assert
        response.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task GetById_WithNonExistingId_ReturnsNotFound() {
        var base64Id = (Base64Guid)Guid.NewGuid();
        _handler.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(NotFound<User>());

        // Act
        var response = await _controller.GetById(base64Id);

        // Assert
        response.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Create_WithValidData_ReturnsUserResponse() {
        // Arrange
        var request = new UserRequest {
            Email = "some.user@email.com",
            FirstName = "Some",
            LastName = "User",
            Birthday = DateOnly.FromDateTime(DateTime.Today.AddYears(-30)),
        };
        var expected = _sample.ToResponse(DateTime.Now);
        _handler.AddAsync(Arg.Any<User>(), Arg.Any<CancellationToken>())
                .Returns(Success(_sample));

        // Act
        var response = await _controller.Create(request);

        // Assert
        var result = response.Should().BeOfType<CreatedAtActionResult>().Subject;
        var content = result.Value.Should().BeOfType<UserResponse>().Subject;
        content.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task Create_WithExistingId_ReturnsConflict() {
        // Arrange
        var request = new UserRequest {
            Email = "some.user@email.com",
            FirstName = "Some",
            LastName = "User",
            Birthday = DateOnly.FromDateTime(DateTime.Today.AddYears(-30)),
        };
        var expected = request.ToDomain();
        _handler.AddAsync(Arg.Any<User>(), Arg.Any<CancellationToken>())
                .Returns(Conflict(expected));

        // Act
        var response = await _controller.Create(request);

        // Assert
        var result = response.Should().BeOfType<ConflictObjectResult>().Subject;
        result.Value.Should().BeOfType<string>(); // just an error message.
    }

    [Fact]
    public async Task Create_WithInvalidData_ReturnsBadRequest() {
        // Act
        var request = new UserRequest {
            Email = null!,
        };
        _handler.AddAsync(Arg.Any<User>(), Arg.Any<CancellationToken>())
                .Returns(Invalid(_sample, "Some error.", "request"));

        var response = await _controller.Create(request);

        // Assert
        var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
        var error = result.Value.Should().BeOfType<SerializableError>().Subject;
        error.Should().HaveCount(1);
    }

    [Fact]
    public async Task Update_WithValidData_ReturnsUserResponse() {
        // Arrange
        var base64Id = (Base64Guid)_sample.Id;
        var request = new UserRequest {
            Email = "some.user@email.com",
            FirstName = "Some",
            LastName = "User",
            Birthday = DateOnly.FromDateTime(DateTime.Today.AddYears(-30)),
        };
        var expected = _sample.ToResponse(DateTime.Now);
        _handler.UpdateAsync(Arg.Any<User>(), Arg.Any<CancellationToken>())
                .Returns(Success(_sample));

        // Act
        var response = await _controller.Update(base64Id, request);

        // Assert
        var result = response.Should().BeOfType<OkObjectResult>().Subject;
        var content = result.Value.Should().BeOfType<UserResponse>().Subject;
        content.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task Update_WithInvalidId_ReturnsBadRequest() {
        // Arrange
        var request = new UserRequest {
            Email = "some.user@email.com",
            FirstName = "Some",
            LastName = "User",
            Birthday = DateOnly.FromDateTime(DateTime.Today.AddYears(-30)),
        };

        // Act
        var response = await _controller.Update("invalid", request);

        // Assert
        response.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task Update_WithNonExistingId_ReturnsNotFound() {
        var base64Id = (Base64Guid)Guid.NewGuid();
        var request = new UserRequest {
            Email = "some.user@email.com",
            FirstName = "Some",
            LastName = "User",
            Birthday = DateOnly.FromDateTime(DateTime.Today.AddYears(-30)),
        };
        _handler.UpdateAsync(Arg.Any<User>(), Arg.Any<CancellationToken>())
                .Returns(NotFound<User>());

        // Act
        var response = await _controller.Update(base64Id, request);

        // Assert
        response.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Update_WithInvalidData_ReturnsBadRequest() {
        // Act
        var base64Id = (Base64Guid)_sample.Id;
        var request = new UserRequest {
            Email = null!,
        };
        _handler.UpdateAsync(Arg.Any<User>(), Arg.Any<CancellationToken>())
                .Returns(Invalid(_sample, "Some error.", "request"));

        var response = await _controller.Update(base64Id, request);

        // Assert
        var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
        var error = result.Value.Should().BeOfType<SerializableError>().Subject;
        error.Should().HaveCount(1);
    }

    [Fact]
    public async Task Remove_WithValidId_ReturnsUserResponse() {
        // Arrange
        var base64Id = (Base64Guid)_sample.Id;
        _handler.RemoveAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(Success);

        // Act
        var response = await _controller.Remove(base64Id);

        // Assert
        response.Should().BeOfType<OkResult>();
    }

    [Fact]
    public async Task Remove_WithInvalidId_ReturnsNotFound() {
        // Act
        var response = await _controller.Remove("invalid");

        // Assert
        response.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task Remove_WithNonExistingId_ReturnsNotFound() {
        var base64Id = (Base64Guid)Guid.NewGuid();
        _handler.RemoveAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(NotFound<User>());

        // Act
        var response = await _controller.Remove(base64Id);

        // Assert
        response.Should().BeOfType<NotFoundResult>();
    }
}

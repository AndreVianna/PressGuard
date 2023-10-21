using RemoteService.Controllers.Systems;
using RemoteService.Controllers.Systems.Models;
using RemoteService.Handlers.System;

using static System.Results.CrudResult;

namespace RemoteService.Controllers;

public class SystemsControllerTests {
    private readonly ISystemHandler _handler = Substitute.For<ISystemHandler>();
    private static readonly ILogger<SystemsController> _logger = Substitute.For<ILogger<SystemsController>>();
    private static readonly SystemRow[] _rows = new[] {
        new SystemRow {
            Id = Guid.NewGuid(),
            Name = "Lairs & Lizards 3e",
        },
        new SystemRow {
            Id = Guid.NewGuid(),
            Name = "RoadScout",
        }
    };
    private static readonly Handlers.System.System _sample = new() {
        Id = Guid.NewGuid(),
        Name = "Lairs & Lizards 3e",
        Description = "A very nice role playing game.",
        ShortName = "LnL3e",
        Tags = new[] { "Fantasy", "Adventure" },
    };

    private readonly SystemsController _controller;

    public SystemsControllerTests() {
        _controller = new SystemsController(_handler, _logger);
    }

    [Fact]
    public async Task GetMany_ReturnsArrayOfGameSystemRowResponses() {
        // Arrange
        var expectedRows = _rows.ToResponse();
        _handler.GetManyAsync(Arg.Any<CancellationToken>()).Returns(Success(_rows.AsEnumerable()));

        // Act
        var response = await _controller.GetMany();

        // Assert
        var result = response.Should().BeOfType<OkObjectResult>().Subject;
        var content = result.Value.Should().BeOfType<SystemRowResponse[]>().Subject;
        content.Should().BeEquivalentTo(expectedRows);
    }

    [Fact]
    public async Task GetById_WithValidId_ReturnsGameSystemResponse() {
        // Arrange
        var expected = _sample.ToResponse();
        var base64Id = (Base64Guid)_sample.Id;
        _handler.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(Success(_sample));

        // Act
        var response = await _controller.GetById(base64Id);

        // Assert
        var result = response.Should().BeOfType<OkObjectResult>().Subject;
        var content = result.Value.Should().BeOfType<SystemResponse>().Subject;
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
                .Returns(NotFound<Handlers.System.System>());

        // Act
        var response = await _controller.GetById(base64Id);

        // Assert
        response.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Create_WithValidData_ReturnsGameSystemResponse() {
        // Arrange
        var request = new SystemRequest {
            Name = "Lairs & Lizards 3e",
            Description = "A very nice role playing game.",
            ShortName = "LnL3e",
            Tags = new[] { "Fantasy", "Adventure" },
        };
        var expected = _sample.ToResponse();
        _handler.AddAsync(Arg.Any<Handlers.System.System>(), Arg.Any<CancellationToken>())
                .Returns(Success(_sample));

        // Act
        var response = await _controller.Create(request);

        // Assert
        var result = response.Should().BeOfType<CreatedAtActionResult>().Subject;
        var content = result.Value.Should().BeOfType<SystemResponse>().Subject;
        content.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task Create_WithExistingId_ReturnsConflict() {
        // Arrange
        var request = new SystemRequest {
            Name = "Lairs & Lizards 3e",
            Description = "A very nice role playing game.",
            ShortName = "LnL3e",
            Tags = new[] { "Fantasy", "Adventure" },
        };
        var expected = request.ToDomain();
        _handler.AddAsync(Arg.Any<Handlers.System.System>(), Arg.Any<CancellationToken>())
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
        var request = new SystemRequest {
            Name = null!,
            Description = null!,
        };
        _handler.AddAsync(Arg.Any<Handlers.System.System>(), Arg.Any<CancellationToken>())
                .Returns(Invalid(_sample, "Some error.", "request"));

        var response = await _controller.Create(request);

        // Assert
        var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
        var error = result.Value.Should().BeOfType<SerializableError>().Subject;
        error.Should().HaveCount(1);
    }

    [Fact]
    public async Task Update_WithValidData_ReturnsGameSystemResponse() {
        // Arrange
        var base64Id = (Base64Guid)_sample.Id;
        var request = new SystemRequest {
            Name = "Lairs & Lizards 3e",
            Description = "A very nice role playing game.",
            ShortName = "LnL3e",
            Tags = new[] { "Fantasy", "Adventure" },
        };
        var expected = _sample.ToResponse();
        _handler.UpdateAsync(Arg.Any<Handlers.System.System>(), Arg.Any<CancellationToken>())
                .Returns(Success(_sample));

        // Act
        var response = await _controller.Update(base64Id, request);

        // Assert
        var result = response.Should().BeOfType<OkObjectResult>().Subject;
        var content = result.Value.Should().BeOfType<SystemResponse>().Subject;
        content.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task Update_WithInvalidId_ReturnsBadRequest() {
        // Arrange
        var request = new SystemRequest {
            Name = "Lairs & Lizards 3e",
            Description = "A very nice role playing game.",
            ShortName = "LnL3e",
            Tags = new[] { "Fantasy", "Adventure" },
        };

        // Act
        var response = await _controller.Update("invalid", request);

        // Assert
        response.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task Update_WithNonExistingId_ReturnsNotFound() {
        var base64Id = (Base64Guid)Guid.NewGuid();
        var request = new SystemRequest {
            Name = "Lairs & Lizards 3e",
            Description = "A very nice role playing game.",
            ShortName = "LnL3e",
            Tags = new[] { "Fantasy", "Adventure" },
        };
        _handler.UpdateAsync(Arg.Any<Handlers.System.System>(), Arg.Any<CancellationToken>())
                .Returns(NotFound<Handlers.System.System>());

        // Act
        var response = await _controller.Update(base64Id, request);

        // Assert
        response.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Update_WithInvalidData_ReturnsBadRequest() {
        // Act
        var base64Id = (Base64Guid)_sample.Id;
        var request = new SystemRequest {
            Name = null!,
            Description = null!,
        };
        _handler.UpdateAsync(Arg.Any<Handlers.System.System>(), Arg.Any<CancellationToken>())
                .Returns(Invalid(_sample, "Some error.", "request"));

        var response = await _controller.Update(base64Id, request);

        // Assert
        var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
        var error = result.Value.Should().BeOfType<SerializableError>().Subject;
        error.Should().HaveCount(1);
    }

    [Fact]
    public async Task Remove_WithValidId_ReturnsGameSystemResponse() {
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
        _handler.RemoveAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(NotFound<Handlers.System.System>());

        // Act
        var response = await _controller.Remove(base64Id);

        // Assert
        response.Should().BeOfType<NotFoundResult>();
    }
}

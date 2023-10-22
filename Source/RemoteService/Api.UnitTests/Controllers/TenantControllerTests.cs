using RemoteService.Controllers.Tenants;
using RemoteService.Controllers.Tenants.Models;
using RemoteService.Handlers.Tenants;
using RemoteService.Models;

using static System.Results.CrudResult;

namespace RemoteService.Controllers;

public class TenantControllerTests {
    private readonly ITenantHandler _handler = Substitute.For<ITenantHandler>();
    private static readonly ILogger<TenantController> _logger = Substitute.For<ILogger<TenantController>>();
    private static readonly TenantRow[] _rows = new[] {
        new TenantRow {
            Id = Guid.NewGuid(),
            Name = "Forest Hospital",
        },
        new TenantRow {
            Id = Guid.NewGuid(),
            Name = "RoadScout",
        }
    };
    private static readonly Tenant _sample = new() {
        Id = Guid.NewGuid(),
        Name = "Forest Hospital",
        Description = "A very nice tenant.",
    };

    private readonly TenantController _controller;

    public TenantControllerTests() {
        _controller = new(_handler, _logger);
    }

    [Fact]
    public async Task GetMany_ReturnsArrayOfTenantRowResponses() {
        // Arrange
        var expectedRows = _rows.ToResponse();
        _handler.GetManyAsync(Arg.Any<CancellationToken>()).Returns(Success(_rows.AsEnumerable()));

        // Act
        var response = await _controller.GetMany();

        // Assert
        var result = response.Should().BeOfType<OkObjectResult>().Subject;
        var content = result.Value.Should().BeOfType<TenantRowResponse[]>().Subject;
        content.Should().BeEquivalentTo(expectedRows);
    }

    [Fact]
    public async Task GetById_WithValidId_ReturnsTenantResponse() {
        // Arrange
        var expected = _sample.ToResponse();
        var base64Id = (Base64Guid)_sample.Id;
        _handler.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(Success(_sample));

        // Act
        var response = await _controller.GetById(base64Id);

        // Assert
        var result = response.Should().BeOfType<OkObjectResult>().Subject;
        var content = result.Value.Should().BeOfType<TenantResponse>().Subject;
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
                .Returns(NotFound<Tenant>());

        // Act
        var response = await _controller.GetById(base64Id);

        // Assert
        response.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Create_WithValidData_ReturnsTenantResponse() {
        // Arrange
        var request = new TenantRequest {
            Name = "Forest Hospital",
            Description = "A very nice tenant.",
        };
        var expected = _sample.ToResponse();
        _handler.AddAsync(Arg.Any<Tenant>(), Arg.Any<CancellationToken>())
                .Returns(Success(_sample));

        // Act
        var response = await _controller.Create(request);

        // Assert
        var result = response.Should().BeOfType<CreatedAtActionResult>().Subject;
        var content = result.Value.Should().BeOfType<TenantResponse>().Subject;
        content.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task Create_WithExistingId_ReturnsConflict() {
        // Arrange
        var request = new TenantRequest {
            Name = "Forest Hospital",
            Description = "A very nice tenant.",
        };
        var expected = request.ToDomain();
        _handler.AddAsync(Arg.Any<Tenant>(), Arg.Any<CancellationToken>())
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
        var request = new TenantRequest {
            Name = null!,
            Description = null!,
        };
        _handler.AddAsync(Arg.Any<Tenant>(), Arg.Any<CancellationToken>())
                .Returns(Invalid(_sample, "Some error.", "request"));

        var response = await _controller.Create(request);

        // Assert
        var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
        var error = result.Value.Should().BeOfType<SerializableError>().Subject;
        error.Should().HaveCount(1);
    }

    [Fact]
    public async Task Update_WithValidData_ReturnsTenantResponse() {
        // Arrange
        var base64Id = (Base64Guid)_sample.Id;
        var request = new TenantRequest {
            Name = "Forest Hospital",
            Description = "A very nice tenant.",
        };
        var expected = _sample.ToResponse();
        _handler.UpdateAsync(Arg.Any<Tenant>(), Arg.Any<CancellationToken>())
                .Returns(Success(_sample));

        // Act
        var response = await _controller.Update(base64Id, request);

        // Assert
        var result = response.Should().BeOfType<OkObjectResult>().Subject;
        var content = result.Value.Should().BeOfType<TenantResponse>().Subject;
        content.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task Update_WithInvalidId_ReturnsBadRequest() {
        // Arrange
        var request = new TenantRequest {
            Name = "Forest Hospital",
            Description = "A very nice tenant.",
        };

        // Act
        var response = await _controller.Update("invalid", request);

        // Assert
        response.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task Update_WithNonExistingId_ReturnsNotFound() {
        var base64Id = (Base64Guid)Guid.NewGuid();
        var request = new TenantRequest {
            Name = "Forest Hospital",
            Description = "A very nice tenant.",
        };
        _handler.UpdateAsync(Arg.Any<Tenant>(), Arg.Any<CancellationToken>())
                .Returns(NotFound<Tenant>());

        // Act
        var response = await _controller.Update(base64Id, request);

        // Assert
        response.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Update_WithInvalidData_ReturnsBadRequest() {
        // Act
        var base64Id = (Base64Guid)_sample.Id;
        var request = new TenantRequest {
            Name = null!,
            Description = null!,
        };
        _handler.UpdateAsync(Arg.Any<Tenant>(), Arg.Any<CancellationToken>())
                .Returns(Invalid(_sample, "Some error.", "request"));

        var response = await _controller.Update(base64Id, request);

        // Assert
        var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
        var error = result.Value.Should().BeOfType<SerializableError>().Subject;
        error.Should().HaveCount(1);
    }

    [Fact]
    public async Task Remove_WithValidId_ReturnsTenantResponse() {
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
        _handler.RemoveAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(NotFound<Tenant>());

        // Act
        var response = await _controller.Remove(base64Id);

        // Assert
        response.Should().BeOfType<NotFoundResult>();
    }
}

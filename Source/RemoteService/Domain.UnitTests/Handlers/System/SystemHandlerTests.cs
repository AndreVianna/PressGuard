using RemoteService.Repositories.System;

namespace RemoteService.Handlers.System;

public class SystemHandlerTests {
    private readonly SystemHandler _handler;
    private readonly ISystemRepository _repository;

    public SystemHandlerTests() {
        _repository = Substitute.For<ISystemRepository>();
        _handler = new(_repository);
    }

    [Fact]
    public async Task GetManyAsync_ReturnsSystems() {
        // Arrange
        var expected = new[] { CreateRow() };
        _repository.GetManyAsync(Arg.Any<CancellationToken>()).Returns(expected);

        // Act
        var result = await _handler.GetManyAsync();

        // Assert
        result.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsSystem() {
        // Arrange
        var id = Guid.NewGuid();
        var expected = CreateInput(id);
        _repository.GetByIdAsync(id, Arg.Any<CancellationToken>()).Returns(expected);

        // Act
        var result = await _handler.GetByIdAsync(id);

        // Assert
        result.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ReturnsNotFound() {
        // Arrange
        var id = Guid.NewGuid();
        _repository.GetByIdAsync(id, Arg.Any<CancellationToken>()).Returns(default(System));

        // Act
        var result = await _handler.GetByIdAsync(id);

        // Assert
        result.WasNotFound.Should().BeTrue();
    }

    [Fact]
    public async Task AddAsync_ReturnsSystem() {
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
        _repository.AddAsync(input, Arg.Any<CancellationToken>()).Returns(default(System));

        // Act
        var result = await _handler.AddAsync(input);

        // Assert
        result.IsInvalid.Should().BeFalse();
        result.HasConflict.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task AddAsync_WithValidationError_ReturnsFailure() {
        // Arrange
        var input = CreateInput();
        input = input with { Name = "" };

        // Act
        var result = await _handler.AddAsync(input);

        // Assert
        result.IsInvalid.Should().BeTrue();
        result.Errors.Should().HaveCount(1);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsSystem() {
        // Arrange
        var id = Guid.NewGuid();
        var input = CreateInput(id);
        _repository.GetByIdAsync(id, Arg.Any<CancellationToken>()).Returns(input);
        _repository.UpdateAsync(input, Arg.Any<CancellationToken>()).Returns(input);

        // Act
        var result = await _handler.UpdateAsync(input);

        // Assert
        result.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateAsync_WithInvalidId_ReturnsNull() {
        // Arrange
        var id = Guid.NewGuid();
        var input = CreateInput(id);
        _repository.UpdateAsync(input, Arg.Any<CancellationToken>()).Returns(default(System));

        // Act
        var result = await _handler.UpdateAsync(input);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.WasNotFound.Should().BeTrue();
        result.Value.Should().BeNull();
    }

    [Fact]
    public async Task UpdateAsync_WithValidationError_ReturnsFailure() {
        // Arrange
        var input = CreateInput();
        input = input with { Name = "" };

        // Act
        var result = await _handler.UpdateAsync(input);

        // Assert
        result.IsInvalid.Should().BeTrue();
        result.Errors.Should().HaveCount(1);
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
    public async Task Remove_WithInvalidId_ReturnsTrue() {
        // Arrange
        var id = Guid.NewGuid();
        _repository.RemoveAsync(id, Arg.Any<CancellationToken>()).Returns(false);

        // Act
        var result = await _handler.RemoveAsync(id);

        // Assert
        result.IsSuccess.Should().BeFalse();
    }

    private static SystemRow CreateRow(Guid? id = null)
        => new() {
            Id = id ?? Guid.NewGuid(),
            Name = "Some Name",
        };

    private static System CreateInput(Guid? id = null)
        => new() {
            Id = id ?? Guid.NewGuid(),
            Name = "Some Name",
            ShortName = "SM",
            Description = "Some description."
        };
}

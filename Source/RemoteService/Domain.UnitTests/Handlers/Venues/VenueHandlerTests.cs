using RemoteService.Repositories.Venues;

namespace RemoteService.Handlers.Venues;

public class VenueHandlerTests {
    private readonly VenueHandler _handler;
    private readonly IVenueRepository _repository;

    public VenueHandlerTests() {
        _repository = Substitute.For<IVenueRepository>();
        _handler = new(_repository);
    }

    [Fact]
    public async Task GetManyAsync_ReturnsSpheres() {
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
    public async Task GetByIdAsync_ReturnsSphere() {
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
        _repository.GetByIdAsync(id, Arg.Any<CancellationToken>()).Returns(default(Venue));

        // Act
        var result = await _handler.GetByIdAsync(id);

        // Assert
        result.WasNotFound.Should().BeTrue();
        result.IsInvalid.Should().BeFalse();
        result.Value.Should().BeNull();
    }

    [Fact]
    public async Task AddAsync_ReturnsSphere() {
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
        _repository.AddAsync(input, Arg.Any<CancellationToken>()).Returns(default(Venue));

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
        var input = new Venue {
            Name = null!,
            Address = new Address {
                Line1 = "42 Infinity street",
                Line2 = "Apt 666",
                City = "Nowhere",
                Province = "NS",
                ZipCode = "A0A 0A0",
            },
            Description = null!,
        };

        // Act
        var result = await _handler.AddAsync(input);

        // Assert
        result.IsInvalid.Should().BeTrue();
        result.HasConflict.Should().BeFalse();
        result.Value.Should().Be(input);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsSphere() {
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
        _repository.UpdateAsync(input, Arg.Any<CancellationToken>()).Returns(default(Venue));

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
        var input = new Venue {
            Name = null!,
            Address = new Address {
                Line1 = "42 Infinity street",
                Line2 = "Apt 666",
                City = "Nowhere",
                Province = "NS",
                ZipCode = "A0A 0A0",
            },
            Description = null!,
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
        _repository.GetByIdAsync(id, Arg.Any<CancellationToken>()).Returns(default(Venue));
        _repository.RemoveAsync(id, Arg.Any<CancellationToken>()).Returns(false);

        // Act
        var result = await _handler.RemoveAsync(id);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.WasNotFound.Should().BeTrue();
    }

    private static VenueRow CreateRow(Guid? id = null)
        => new() {
            Id = id ?? Guid.NewGuid(),
            Name = "Some Name",
        };

    private static Venue CreateInput(Guid? id = null)
        => new() {
            Id = id ?? Guid.NewGuid(),
            Name = "Some Name",
            Address = new Address {
                Line1 = "42 Infinity street",
                Line2 = "Apt 666",
                City = "Nowhere",
                Province = "NS",
                ZipCode = "A0A 0A0",
            },
            Description = "Some description.",
        };
}

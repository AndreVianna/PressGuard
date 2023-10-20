using GameSystem = RemoteService.Handlers.System.System;

namespace DataAccess.Repositories.Systems;

public class SystemRepositoryTests {
    private readonly IJsonFileStorage<SystemData> _storage;
    private readonly SystemRepository _repository;

    public SystemRepositoryTests() {
        _storage = Substitute.For<IJsonFileStorage<SystemData>>();
        var userAccessor = Substitute.For<IUserAccessor>();
        userAccessor.BaseFolder.Returns("User1234");
        _repository = new(_storage, userAccessor);
    }

    [Fact]
    public async Task GetManyAsync_ReturnsAll() {
        // Arrange
        var dataFiles = GenerateList();
        _storage.GetAllAsync().Returns(dataFiles);

        // Act
        var settings = await _repository.GetManyAsync();

        // Assert
        settings.Should().HaveCount(dataFiles.Length);
    }

    [Fact]
    public async Task GetByIdAsync_SystemFound_ReturnsSystem() {
        // Arrange
        var dataFile = GenerateData();
        var tokenSource = new CancellationTokenSource();
        _storage.GetByIdAsync(dataFile.Id, tokenSource.Token).Returns(dataFile);

        // Act
        var setting = await _repository.GetByIdAsync(dataFile.Id, tokenSource.Token);

        // Assert
        setting.Should().NotBeNull();
    }

    [Fact]
    public async Task GetByIdAsync_SystemNotFound_ReturnsNull() {
        // Arrange
        var id = Guid.NewGuid();
        _storage.GetByIdAsync(id, Arg.Any<CancellationToken>()).Returns(default(SystemData));

        // Act
        var setting = await _repository.GetByIdAsync(id);

        // Assert
        setting.Should().BeNull();
    }

    [Fact]
    public async Task InsertAsync_InsertsNewSystem() {
        // Arrange
        var id = Guid.NewGuid();
        var input = GenerateInput(id);
        var expected = GenerateData(id);
        _storage.CreateAsync(Arg.Any<SystemData>()).Returns(expected);

        // Act
        var result = await _repository.AddAsync(input);

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateAsync_UpdatesExistingSystem() {
        // Arrange
        var id = Guid.NewGuid();
        var input = GenerateInput(id);
        var expected = GenerateData(id);
        _storage.UpdateAsync(Arg.Any<SystemData>()).Returns(expected);

        // Act
        var result = await _repository.UpdateAsync(input);

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateAsync_WithNonExistingId_ReturnsNull() {
        // Arrange
        var id = Guid.NewGuid();
        var input = GenerateInput(id);
        _storage.UpdateAsync(Arg.Any<SystemData>()).Returns(default(SystemData));

        // Act
        var result = await _repository.UpdateAsync(input);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task RemoveAsync_RemovesDomain() {
        // Arrange
        var id = Guid.NewGuid();
        _storage.Delete(id).Returns(true);

        // Act
        var result = await _repository.RemoveAsync(id);

        // Assert
        result.Should().BeTrue();
    }

    private static SystemData[] GenerateList()
        => new[] { GenerateData() };

    private static SystemData GenerateData(Guid? id = null)
        => new() {
            Id = id ?? Guid.NewGuid(),
            State = State.New,
            ShortName = "SomeId",
            Name = "Some Id",
            Description = "Some Description",
            Tags = new[] { "SomeTag" },
        };

    private static GameSystem GenerateInput(Guid? id = null)
        => new() {
            Id = id ?? Guid.NewGuid(),
            State = State.New,
            ShortName = "SomeId",
            Name = "Some Id",
            Description = "Some Description",
            Tags = new[] { "SomeTag" },
            Domains = new[] {
                new Base {
                    Name = "Dom1",
                    Description = "SomeDescription",
                },
                new Base {
                    Name = "Domain1",
                    ShortName = "Dom2",
                    Description = "SomeDescription",
                }
            },
        };
}

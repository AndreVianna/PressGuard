namespace RemoteService.Repositories.Venues;

public class VenueRepositoryTests {
    private readonly IJsonFileStorage<VenueData> _storage;
    private readonly VenueRepository _repository;

    public VenueRepositoryTests() {
        _storage = Substitute.For<IJsonFileStorage<VenueData>>();
        var userAccessor = Substitute.For<IUserAccessor>();
        userAccessor.BaseFolder.Returns("User1234");
        _repository = new(_storage, userAccessor);
    }

    [Fact]
    public async Task GetManyAsync_ReturnsAllDomains() {
        // Arrange
        var dataFiles = GenerateList();
        _storage.GetAllAsync().Returns(dataFiles);

        // Act
        var domains = await _repository.GetManyAsync();

        // Assert
        domains.Should().HaveCount(dataFiles.Length);
    }

    [Fact]
    public async Task GetByIdAsync_DomainFound_ReturnsDomain() {
        // Arrange
        var dataFile = GenerateData();
        var tokenSource = new CancellationTokenSource();
        _storage.GetByIdAsync(dataFile.Id, tokenSource.Token).Returns(dataFile);

        // Act
        var domain = await _repository.GetByIdAsync(dataFile.Id, tokenSource.Token);

        // Assert
        domain.Should().NotBeNull();
    }

    [Fact]
    public async Task GetByIdAsync_DomainNotFound_ReturnsNull() {
        // Arrange
        var id = Guid.NewGuid();
        _storage.GetByIdAsync(id, Arg.Any<CancellationToken>()).Returns(default(VenueData));

        // Act
        var domain = await _repository.GetByIdAsync(id);

        // Assert
        domain.Should().BeNull();
    }

    [Fact]
    public async Task InsertAsync_InsertsNewDomain() {
        // Arrange
        var id = Guid.NewGuid();
        var domain = GenerateInput(id);
        var expected = GenerateData(id);
        var tokenSource = new CancellationTokenSource();
        _storage.CreateAsync(Arg.Any<VenueData>(), tokenSource.Token).Returns(expected);

        // Act
        var result = await _repository.AddAsync(domain, tokenSource.Token);

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateAsync_UpdatesExistingDomain() {
        // Arrange
        var id = Guid.NewGuid();
        var domain = GenerateInput(id);
        var expected = GenerateData(id);
        var tokenSource = new CancellationTokenSource();
        _storage.UpdateAsync(Arg.Any<VenueData>(), tokenSource.Token).Returns(expected);

        // Act
        var result = await _repository.UpdateAsync(domain, tokenSource.Token);

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateAsync_WithNonExistingId_ReturnsNull() {
        // Arrange
        var id = Guid.NewGuid();
        var domain = GenerateInput(id);
        var tokenSource = new CancellationTokenSource();
        _storage.UpdateAsync(Arg.Any<VenueData>(), tokenSource.Token).Returns(default(VenueData));

        // Act
        var result = await _repository.UpdateAsync(domain, tokenSource.Token);

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

    private static VenueData[] GenerateList()
        => new[] { GenerateData() };

    private static VenueData GenerateData(Guid? id = null)
        => new() {
            Id = id ?? Guid.NewGuid(),
            State = State.New,
            Name = "Some Id",
            Description = "Some Description",
            Address = new AddressData {
                Line1 = "42 Infinity street",
                Line2 = "Apt 666",
                City = "Nowhere",
                Province = "NS",
                ZipCode = "A0A 0A0",
            },
            Devices = new DeviceData[] {
                new() {
                    Port = 42,
                    Name = "Some Name",
                    Description = "Some device.",
                    Sensors = new SensorData[] {
                        new() {
                            Model = "Some Mode.",
                        },
                    },
                },
            },
        };

    private static Venue GenerateInput(Guid? id = null)
        => new() {
            Id = id ?? Guid.NewGuid(),
            Address = new Address {
                Line1 = "42 Infinity street",
                Line2 = "Apt 666",
                City = "Nowhere",
                Province = "NS",
                ZipCode = "A0A 0A0",
            },
            State = State.New,
            Name = "Some Id",
            Description = "Some Description",
            Devices = new Device[] {
                new() {
                    Port = 42,
                    Name = "Some Name",
                    Description = "Some device.",
                    Sensors = new Sensor[] {
                        new() {
                            Model = "Some Mode.",
                        },
                    },
                },
            },
        };
}

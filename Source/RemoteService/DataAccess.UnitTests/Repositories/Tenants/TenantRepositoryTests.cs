using RemoteService.Repositories.Venues;

namespace RemoteService.Repositories.Tenants;

public class TenantRepositoryTests {
    private readonly IJsonFileStorage<TenantData> _storage;
    private readonly TenantRepository _repository;

    public TenantRepositoryTests() {
        _storage = Substitute.For<IJsonFileStorage<TenantData>>();
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
        _storage.GetByIdAsync(id, Arg.Any<CancellationToken>()).Returns(default(TenantData));

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
        _storage.CreateAsync(Arg.Any<TenantData>()).Returns(expected);

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
        _storage.UpdateAsync(Arg.Any<TenantData>()).Returns(expected);

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
        _storage.UpdateAsync(Arg.Any<TenantData>()).Returns(default(TenantData));

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

    private static TenantData[] GenerateList()
        => new[] { GenerateData() };

    private static TenantData GenerateData(Guid? id = null)
        => new() {
            Id = id ?? Guid.NewGuid(),
            State = State.New,
            Name = "Some Id",
            Description = "Some Description",
            Venues = new VenueData[] {
                new() {
                    Name = "Venue1",
                    Address = new AddressData {
                        Line1 = "42 Infinity street",
                        Line2 = "Apt 666",
                        City = "Nowhere",
                        Province = "NS",
                        ZipCode = "A0A 0A0",
                    },
                    Description = "SomeDescription",
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
                },
                new() {
                    Name = "Venue2",
                    Address = new AddressData {
                        Line1 = "42 Infinity street",
                        Line2 = "Apt 666",
                        City = "Nowhere",
                        Province = "NS",
                        ZipCode = "A0A 0A0",
                    },
                    Description = "SomeDescription",
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
                }
            }
        };

    private static Tenant GenerateInput(Guid? id = null)
        => new() {
            Id = id ?? Guid.NewGuid(),
            State = State.New,
            Name = "Some Id",
            Description = "Some Description",
            Venues = new[] {
                new Venue {
                    Name = "Dom1",
                    Address = new Address {
                        Line1 = "42 Infinity street",
                        Line2 = "Apt 666",
                        City = "Nowhere",
                        Province = "NS",
                        ZipCode = "A0A 0A0",
                    },
                    Description = "SomeDescription",
                },
                new Venue {
                    Name = "Domain1",
                    Address = new Address {
                        Line1 = "42 Infinity street",
                        Line2 = "Apt 666",
                        City = "Nowhere",
                        Province = "NS",
                        ZipCode = "A0A 0A0",
                    },
                    Description = "SomeDescription",
                }
            },
        };
}

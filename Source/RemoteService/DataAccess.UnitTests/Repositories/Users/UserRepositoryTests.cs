namespace RemoteService.Repositories.Users;

public class UserRepositoryTests {
    private readonly IJsonFileStorage<UserData> _storage;
    private readonly UserRepository _repository;

    private readonly UserData _user1;

    public UserRepositoryTests() {
        var hasher = Substitute.For<IHasher>();
        var hashedSecret = new HashedSecret("ValidHashedPassword="u8.ToArray(), "SomeSalt"u8.ToArray());
        var invalidSecret = new HashedSecret("WrongHashedPassword="u8.ToArray(), "SomeSalt"u8.ToArray());
        hasher.HashSecret(Arg.Any<string>(), Arg.Any<byte[]>()).Returns(invalidSecret);
        hasher.HashSecret("ValidPassword", Arg.Any<byte[]>()).Returns(hashedSecret);

        _user1 = new() {
            Id = Guid.Parse("26409801-0d28-4df4-97e9-53b678919416"),
            Email = "some.user@email.com",
            HashedPassword = hashedSecret,
            FirstName = "Some",
            LastName = "User",
            Birthday = DateOnly.FromDateTime(DateTime.Today.AddYears(-30)),
        };
        UserData user2 = new() {
            Id = Guid.Parse("57e48beb-8e3d-4996-bbea-885515591c3d"),
            Email = "other.user@email.com",
        };
        UserData user3 = new() {
            Id = Guid.Parse("16290ee9-70a6-4e34-a03c-b397bb97407e"),
            Email = "temp.user@email.com",
        };

        _storage = Substitute.For<IJsonFileStorage<UserData>>();
        _storage.GetAllAsync(null, Arg.Any<CancellationToken>()).Returns(new [] { _user1, user2, user3 });
        _storage.GetByIdAsync(_user1.Id, Arg.Any<CancellationToken>()).Returns(_user1);
        _storage.GetByIdAsync(user2.Id, Arg.Any<CancellationToken>()).Returns(user2);
        _storage.GetByIdAsync(user3.Id, Arg.Any<CancellationToken>()).Returns(user3);

        _repository = new(_storage, hasher);
    }

    [Theory]
    [InlineData("some.user@email.com", "ValidPassword", true)]
    [InlineData("some.user@email.com", "WrongPassword", false)]
    [InlineData("other.user@email.com", "AnyPassword", false)]
    [InlineData("wrong.user@email.com", "ValidPassword", false)]
    public async Task VerifyAsync_WithValidSecret_ReturnsSuccess(string email, string password, bool isValid) {
        // Arrange
        var signIn = new SignIn {
            Email = email,
            Password = password,
        };
        var tokenSource = new CancellationTokenSource();

        // Act
        var result = await _repository.VerifyAsync(signIn, tokenSource.Token);

        // Assert
        if (isValid) result.Should().NotBeNull();
        else result.Should().BeNull();
    }

    [Fact]
    public async Task GetManyAsync_ReturnsAll() {
        // Act
        var users = await _repository.GetManyAsync();

        // Assert
        users.Should().HaveCount(3);
    }

    [Fact]
    public async Task GetByIdAsync_UserFound_ReturnsUser() {
        // Arrange
        var tokenSource = new CancellationTokenSource();

        // Act
        var user = await _repository.GetByIdAsync(_user1.Id, tokenSource.Token);

        // Assert
        user.Should().NotBeNull();
    }

    [Fact]
    public async Task GetByIdAsync_UserNotFound_ReturnsNull() {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var user = await _repository.GetByIdAsync(id);

        // Assert
        user.Should().BeNull();
    }

    [Fact]
    public async Task InsertAsync_InsertsNewUser() {
        // Arrange
        var newUser1 = new UserData {
            Id = Guid.Parse("e3ade862-addf-4628-88f5-ed0938ca91c9"),
            Email = "new.user@email.com",
            HashedPassword = new("NewHashedPassword="u8.ToArray(), "SomeSalt"u8.ToArray()),
            FirstName = "New",
            LastName = "User",
            Birthday = DateOnly.FromDateTime(DateTime.Today.AddYears(-30)),
        };
        var input = newUser1.ToModel()!;
        var expected = newUser1;
        _storage.CreateAsync(Arg.Any<UserData>()).Returns(expected);

        // Act
        var result = await _repository.AddAsync(input);

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task InsertAsync_WithDuplicatedUser_ReturnsNull() {
        // Arrange
        var input = _user1.ToModel()!;

        // Act
        var result = await _repository.AddAsync(input);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task UpdateAsync_UpdatesExistingUser() {
        // Arrange
        var input = _user1.ToModel()!;
        var expected = _user1;
        _storage.UpdateAsync(Arg.Any<UserData>()).Returns(expected);

        // Act
        var result = await _repository.UpdateAsync(input);

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateAsync_WithNonExistingId_ReturnsNull() {
        // Arrange
        var input = _user1.ToModel()!;
        _storage.UpdateAsync(Arg.Any<UserData>()).Returns(default(UserData));

        // Act
        var result = await _repository.UpdateAsync(input);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task RemoveAsync_RemovesDomain() {
        // Arrange
        var id = _user1.Id;
        _storage.Delete(id).Returns(true);

        // Act
        var result = await _repository.RemoveAsync(id);

        // Assert
        result.Should().BeTrue();
    }
}

namespace RemoteService.Handlers.Auth;

public class UserTests {
    [Fact]
    public void Constructor_CreatesInstance() {
        var agent = new User {
            Id = Guid.NewGuid(),
            Email = "some.user@email.com",
            FirstName = "Some",
            LastName = "User",
            Birthday = DateOnly.FromDateTime(DateTime.Today.AddYears(-30)),
        };

        agent.Should().NotBeNull();
    }

    [Fact]
    public void Validate_Validates() {
        var testBase = new User {
            Id = Guid.NewGuid(),
            Email = "some.user@email.com",
            FirstName = "Some",
            LastName = "User",
            Birthday = DateOnly.FromDateTime(DateTime.Today.AddYears(-30)),
        };

        var result = testBase.Validate();

        result.IsSuccess.Should().BeTrue();
        result.Errors.Should().HaveCount(0);
    }

    [Fact]
    public void Validate_WithErrors_Validates() {
        var subject = new User {
            Id = Guid.NewGuid(),
            Email = "invalid",
        };

        var result = subject.Validate();

        result.IsSuccess.Should().BeFalse();
        result.Errors.Select(i => i.Message).Should().BeEquivalentTo(
        "'Email' must be a valid email.");
    }
}
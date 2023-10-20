using RemoteService.Constants;

namespace RemoteService.Handlers.Auth;

public class LoginTests {
    [Fact]
    public void Constructor_CreatesInstance() {
        var login = new SignIn {
            Email = "user.name@email.com",
            Password = "SomePassword",
        };

        login.Should().NotBeNull();
        login.Email.Should().Be("user.name@email.com");
        login.Password.Should().Be("SomePassword");
    }

    private class TestData : TheoryData<SignIn, int> {
        public TestData() {
            Add(new() { Email = "user.name@email.com", Password = "SomePassword" }, 0);
            Add(new() { Email = null!, Password = null! }, 2);
            Add(new() { Email = "", Password = "" }, 3);
            Add(new() { Email = "   ", Password = "   " }, 3);
            Add(new() { Email = "invalid", Password = new('X', Validation.Password.MaximumLength + 1) }, 2);
        }
    }

    [Theory]
    [ClassData(typeof(TestData))]
    public void Validate_Validates(SignIn subject, int errorCount) {
        // Act
        var result = subject.Validate();

        // Assert
        result.Errors.Should().HaveCount(errorCount);
    }
}
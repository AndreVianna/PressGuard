namespace RemoteService.Models;

public class BaseTests {
    private record TestBase : Base;

    [Fact]
    public void Constructor_CreatesInstance() {
        var subject = new TestBase {
            Name = "TestBase",
            Description = "Test base.",
        };

        subject.Name.Should().Be("TestBase");
        subject.Description.Should().Be("Test base.");
    }
}

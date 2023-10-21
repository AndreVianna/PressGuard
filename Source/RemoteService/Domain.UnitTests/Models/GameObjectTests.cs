namespace RemoteService.Models;

public class GameObjectTests {
    [Fact]
    public void Constructor_CreatesInstance() {
        var item = new GameObject {
            Name = "TestName",
            Description = "TestDescription",
            Unit = "m",
        };

        item.Should().NotBeNull();
        item.Unit.Should().Be("m");
    }
}
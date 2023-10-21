namespace RemoteService.Handlers.System;

public class SystemRowTests {
    [Fact]
    public void Constructor_CreatesInstance() {
        var id = Guid.NewGuid();
        var row = new SystemRow {
            Id = id,
            Name = "System Name",
        };

        row.Should().NotBeNull();
        row.Id.Should().Be(id);
        row.Name.Should().Be("System Name");
    }
}
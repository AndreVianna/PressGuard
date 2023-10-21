namespace RemoteService.Handlers.Setting;

public class SettingRowTests {
    [Fact]
    public void Constructor_CreatesInstance() {
        var id = Guid.NewGuid();
        var row = new SettingRow {
            Id = id,
            Name = "Setting Name"
        };

        row.Should().NotBeNull();
        row.Id.Should().Be(id);
        row.Name.Should().Be("Setting Name");
    }
}

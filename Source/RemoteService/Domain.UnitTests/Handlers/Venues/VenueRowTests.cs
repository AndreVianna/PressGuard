namespace RemoteService.Handlers.Venues;

public class VenueRowTests {
    [Fact]
    public void Constructor_CreatesInstance() {
        var id = Guid.NewGuid();
        var row = new VenueRow {
            Id = id,
            Name = "Venue Name"
        };

        row.Should().NotBeNull();
        row.Id.Should().Be(id);
        row.Name.Should().Be("Venue Name");
    }
}

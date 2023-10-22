namespace RemoteService.Handlers.Tenants;

public class TenantRowTests {
    [Fact]
    public void Constructor_CreatesInstance() {
        var id = Guid.NewGuid();
        var row = new TenantRow {
            Id = id,
            Name = "Tenant Name",
        };

        row.Should().NotBeNull();
        row.Id.Should().Be(id);
        row.Name.Should().Be("Tenant Name");
    }
}
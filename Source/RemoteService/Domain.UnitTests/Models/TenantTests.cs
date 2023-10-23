namespace RemoteService.Models;

public class TenantTests {
    [Fact]
    public void Constructor_CreatesInstance() {
        var system = new Tenant {
            Name = "TestName",
            Description = "TestDescription",
            Venues = new List<Venue> {
                new() {
                    Name = "TestDomainName",
                    Address = new Address {
                        Line1 = "42 Infinity street",
                        Line2 = "Apt 666",
                        City = "Nowhere",
                        Province = "NS",
                        ZipCode = "A0A 0A0",
                    },
                    Description = "TestDomainDescription",
                }
            },
        };

        system.Should().NotBeNull();
        system.Venues.Should().HaveCount(1);
    }
}

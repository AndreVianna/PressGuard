using RemoteService.Models;

namespace RemoteService.Handlers.System;

public class SystemTests {
    [Fact]
    public void Constructor_CreatesInstance() {
        var system = new System {
            Name = "TestName",
            Description = "TestDescription",
            Domains = new List<Base> {
                new() {
                    Name = "TestDomainName",
                    Description = "TestDomainDescription",
                }
            },
        };

        system.Should().NotBeNull();
        system.Domains.Should().HaveCount(1);
    }
}
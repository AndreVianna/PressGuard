namespace RemoteService.Models;

public class VenueTests {
    [Fact]
    public void Constructor_CreatesInstance() {
        var agent = new Venue {
            Name = "TestName",
            Address = new Address {
                Line1 = "42 Infinity street",
                Line2 = "Apt 666",
                City = "Nowhere",
                Province = "NS",
                ZipCode = "A0A 0A0",
            },
            Description = "TestDescription"
        };

        agent.Should().NotBeNull();
    }

    [Fact]
    public void Validate_Validates() {
        var devices = new List<Device> {
            new() {
                Port = 42,
                Name = "TestComponent1",
                Description = "TestDescription1",
            },
            new() {
                Port = 7,
                Name = "TestComponent2",
                Description = "TestDescription2",
            },
        };
        var testBase = new Venue {
            Name = "TestName",
            Address = new Address {
                Line1 = "42 Infinity street",
                Line2 = "Apt 666",
                City = "Nowhere",
                Province = "NS",
                ZipCode = "A0A 0A0",
            },
            Description = "TestDescription",
            Devices = devices,
        };

        var result = testBase.Validate();

        result.IsSuccess.Should().BeTrue();
        result.Errors.Should().HaveCount(0);
    }

    [Fact]
    public void Validate_WithErrors_Validates() {
        var components = new List<Device> {
            new() {
                Port = 42,
                Name = null!,
                Description = "TestDescription1",
            },
            null!,
        };
        var subject = new Venue {
            Name = "TestName",
            Address = new Address {
                Line1 = "42 Infinity street",
                Line2 = "Apt 666",
                City = "Nowhere",
                Province = "NS",
                ZipCode = "A0A 0A0",
            },
            Description = "TestDescription",
            Devices = components,
        };

        var result = subject.Validate();

        result.IsSuccess.Should().BeFalse();
        result.Errors.Select(i => i.Message).Should().BeEquivalentTo(
        "'Devices[0].Name' cannot be null.",
        "'Devices[1]' cannot be null.");
    }
}

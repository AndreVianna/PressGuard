namespace RemoteService.Models;

public class SensorTests {
    [Fact]
    public void Constructor_InitializesProperties() {
        var subject = new Sensor {
            Model = "42",
        };

        subject.Model.Should().Be("42");
    }

    [Fact]
    public void Validate_Validates() {
        var testBase = new Sensor {
            Model= "TestModel",
        };

        var result = testBase.Validate();

        result.IsSuccess.Should().BeTrue();
        result.Errors.Should().HaveCount(0);
    }

    [Fact]
    public void Validate_WithErrors_Validates() {
        var subject = new Sensor {
            Model = "",
        };

        var result = subject.Validate();

        result.IsSuccess.Should().BeFalse();
        result.Errors.Select(i => i.Message).Should()
            .BeEquivalentTo("'Model' cannot be empty or whitespace.");
    }
}

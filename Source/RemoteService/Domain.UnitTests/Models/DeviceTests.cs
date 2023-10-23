namespace RemoteService.Models;

public class DeviceTests {
    [Fact]
    public void Constructor_CreatesInstance() {
        var subject = new Device {
            Port = 42,
            Name = "TestSensor",
            Description = "Test sensor.",
            Sensors = new[] {
                new Sensor {
                    Model = "SomeModel",
                },
            },
        };

        subject.Sensors.Should().HaveCount(1);
    }
}

namespace System.Extensions;

public class DateTimeExtensionsTests {
    [Fact]
    public void IsRequired_ReturnsConnector() {
        // Arrange
        var subject = DateTime.Now;

        // Act
        var result = subject.Is();

        // Assert
        result.Should().BeOfType<Connector<DateTime?, DateTimeValidator>>();
    }

    [Fact]
    public void IsRequired_ForNullable_ReturnsConnector() {
        // Arrange
        DateTime? subject = default;

        // Act
        var result = subject.IsRequired();

        // Assert
        result.Should().BeOfType<Connector<DateTime?, DateTimeValidator>>();
    }

    [Fact]
    public void IsOptional_ForNullable_ReturnsConnector() {
        // Arrange
        DateTime? subject = default;

        // Act
        var result = subject.IsOptional();

        // Assert
        result.Should().BeOfType<Connector<DateTime?, DateTimeValidator>>();
    }
}
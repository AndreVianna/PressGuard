namespace System.Extensions;

public class ObjectExtensionsTests {
    [Fact]
    public void IsRequired_ReturnsConnector() {
        // Arrange
        object subject = 42.0m;

        // Act
        var result = subject.IsRequired();

        // Assert
        result.Should().BeOfType<Connector<object?, ObjectValidator>>();
    }

    [Fact]
    public void IsRequired_ForNullable_ReturnsConnector() {
        // Arrange
        object? subject = default;

        // Act
        var result = subject.IsRequired();

        // Assert
        result.Should().BeOfType<Connector<object?, ObjectValidator>>();
    }

    [Fact]
    public void IsOptional_ForNullable_ReturnsConnector() {
        // Arrange
        object? subject = default;

        // Act
        var result = subject.IsOptional();

        // Assert
        result.Should().BeOfType<Connector<object?, ObjectValidator>>();
    }
}
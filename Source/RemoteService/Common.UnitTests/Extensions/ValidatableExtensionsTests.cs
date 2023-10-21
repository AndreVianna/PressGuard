namespace System.Extensions;

public class ValidatableExtensionsTests {
    private class TestValidatable : IValidatable {
        public Result Validate(IDictionary<string, object?>? context = null)
            => throw new NotImplementedException();
    }

    [Fact]
    public void IsRequired_ReturnsConnector() {
        // Arrange
        var subject = new TestValidatable();

        // Act
        var result = subject.IsRequired();

        // Assert
        result.Should().BeOfType<Connector<IValidatable?, ValidatableValidator>>();
    }

    [Fact]
    public void IsRequired_ForNullable_ReturnsConnector() {
        // Arrange
        IValidatable? subject = default;

        // Act
        var result = subject.IsRequired();

        // Assert
        result.Should().BeOfType<Connector<IValidatable?, ValidatableValidator>>();
    }

    [Fact]
    public void IsOptional_ForNullable_ReturnsConnector() {
        // Arrange
        IValidatable? subject = default;

        // Act
        var result = subject.IsOptional();

        // Assert
        result.Should().BeOfType<Connector<IValidatable?, ValidatableValidator>>();
    }
}

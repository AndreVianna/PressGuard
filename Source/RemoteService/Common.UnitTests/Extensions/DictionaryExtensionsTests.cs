namespace System.Extensions;

public class DictionaryExtensionsTests {
    [Fact]
    public void IsRequired_ReturnsConnector() {
        // Arrange
        var subject = new Dictionary<int, string?>();

        // Act
        var result = subject.Is();

        // Assert
        result.Should().BeOfType<Connector<IDictionary<int, string?>, DictionaryValidator<int, string>>>();
    }

    [Fact]
    public void CheckIfEach_ReturnsConnector() {
        // Arrange
        var subject = new Dictionary<int, string?>();

        // Act
        var result = subject.CheckIfEach(i => i.IsRequired());

        // Assert
        result.Should().BeOfType<Connector<IDictionary<int, string?>, DictionaryValidator<int, string>>>();
    }
}
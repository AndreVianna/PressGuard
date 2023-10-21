namespace System.Extensions;

public class CollectionExtensionsTests {
    [Fact]
    public void IsRequired_ReturnsConnector() {
        // Arrange
        var subject = new List<int>();

        // Act
        var result = subject.Is();

        // Assert
        result.Should().BeOfType<Connector<ICollection<int>, CollectionValidator<int>>>();
    }

    [Fact]
    public void CheckIfEach_ReturnsConnector() {
        // Arrange
        var subject = new List<int>();

        // Act
        var result = subject.CheckIfEach(i => i.Is());

        // Assert
        result.Should().BeOfType<Connector<ICollection<int>, CollectionValidator<int>>>();
    }
}

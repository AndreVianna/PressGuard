namespace RemoteService.Models.Attributes;

public class AttributeDefinitionTests {
    [Fact]
    public void Constructor_InitializesProperties() {
        var attribute = new AttributeDefinition {
            Name = "TestAttribute",
            ShortName = "TA",
            Description = "Some description.",
            DataType = typeof(int),
        };

        attribute.Name.Should().Be("TestAttribute");
        attribute.ShortName.Should().Be("TA");
        attribute.Description.Should().Be("Some description.");
        attribute.DataType.Should().Be(typeof(int));
        attribute.Constraints.Should().BeEmpty();
    }
}
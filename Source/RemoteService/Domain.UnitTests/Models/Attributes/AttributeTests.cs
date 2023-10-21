using RemoteService.Models.Abstractions;

namespace RemoteService.Models.Attributes;

public class AttributeTests {
    [Fact]
    public void Constructor_InitializesProperties() {
        var attribute = new AttributeDefinition {
            Name = "TestName",
            Description = "TestDescription",
            DataType = typeof(int),
        };

        IAttribute subject = new NumberAttribute<int> {
            Definition = attribute,
            Value = 42
        };

        subject.Definition.Should().Be(attribute);
        subject.Value.Should().Be(42);
    }
}
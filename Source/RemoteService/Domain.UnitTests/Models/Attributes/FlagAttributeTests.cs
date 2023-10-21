namespace RemoteService.Models.Attributes;

public class FlagAttributeTests {
    private readonly AttributeDefinition _definition;
    private readonly FlagAttribute _attribute;

    public FlagAttributeTests() {
        _definition = new() {
            Name = "TestName",
            Description = "TestDescription",
            DataType = typeof(bool),
        };

        _attribute = new() {
            Definition = _definition,
            Value = true
        };
    }

    [Fact]
    public void Constructor_InitializesProperties() {
        _attribute.Definition.Should().Be(_definition);
        _attribute.Value.Should().Be(true);
        _attribute.Validate().IsSuccess.Should().BeTrue();
    }
}
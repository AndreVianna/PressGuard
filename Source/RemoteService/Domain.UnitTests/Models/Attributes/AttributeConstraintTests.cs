namespace RemoteService.Models.Attributes;

public class AttributeConstraintTests {
    [Fact]
    public void Constructor_InitializesProperties() {
        var attribute = new AttributeConstraint("TestValidator");

        attribute.ValidatorName.Should().Be("TestValidator");
        attribute.Arguments.Should().BeEmpty();
    }

    [Fact]
    public void Constructor_WithoutShortName_InitializesProperties() {
        var attribute = new AttributeConstraint("TestValidator", 20, "String");

        attribute.ValidatorName.Should().Be("TestValidator");
        attribute.Arguments.Should().BeEquivalentTo(new object?[] { 20, "String" });
    }
}
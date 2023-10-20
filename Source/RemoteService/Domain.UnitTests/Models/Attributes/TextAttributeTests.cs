namespace RemoteService.Models.Attributes;

public class TextAttributeTests {
    private readonly AttributeDefinition _definition;
    private readonly TextAttribute _attribute;

    public TextAttributeTests() {
        _definition = new() {
            Name = "TestName",
            Description = "TestDescription",
            DataType = typeof(string)
        };

        _attribute = new() {
            Definition = _definition,
            Value = "TestValue"
        };
    }

    [Fact]
    public void Constructor_InitializesProperties() {
        _attribute.Definition.Should().Be(_definition);
        _attribute.Value.Should().Be("TestValue");
        _attribute.Validate().IsSuccess.Should().BeTrue();
    }

    private class TestData : TheoryData<string, object[], bool> {
        public TestData() {
            Add("LengthIsAtMost", new object[] { 20 }, true);
            Add("LengthIsAtMost", new object[] { 2 }, false);
            Add("LengthIsAtLeast", new object[] { 2 }, true);
            Add("LengthIsAtLeast", new object[] { 20 }, false);
            Add("LengthIs", new object[] { 9 }, true);
            Add("LengthIs", new object[] { 20 }, false);
            Add("IsIn", new object[] { "One", "TestValue", "Three" }, true);
            Add("IsIn", new object[] { "One", "Two", "Three" }, false);
        }
    }

    [Theory]
    [ClassData(typeof(TestData))]
    public void Validate_WithValidConstraint_ReturnsTrue(string validator, object[] arguments, bool expectedResult) {
        _definition.Constraints.Add(new AttributeConstraint(validator, arguments));

        _attribute.Validate().IsSuccess.Should().Be(expectedResult);
    }

    [Fact]
    public void Validate_WithInvalidArgument_ThrowsArgumentException() {
        _definition.Constraints.Add(new AttributeConstraint("LengthIs", "wrong"));

        _attribute.Invoking(x => x.Validate()).Should().Throw<ArgumentException>().WithMessage("Invalid type of arguments[0] of 'LengthIs'. Expected: Integer. Found: String. (Parameter 'arguments[0]')");
    }

    [Fact]
    public void Validate_WithInvalidNumberOfArguments_ThrowsArgumentException() {
        _definition.Constraints.Add(new AttributeConstraint("LengthIs"));

        _attribute.Invoking(x => x.Validate()).Should().Throw<ArgumentException>().WithMessage("Invalid number of arguments for 'LengthIs'. Missing argument 0. (Parameter 'arguments')");
    }

    [Fact]
    public void Validate_WithInvalidConstraint_ThrowsArgumentException() {
        _definition.Constraints.Add(new AttributeConstraint("Invalid"));

        _attribute.Invoking(x => x.Validate()).Should().Throw<InvalidOperationException>().WithMessage("Unsupported command 'Invalid' for type 'String'.");
    }
}
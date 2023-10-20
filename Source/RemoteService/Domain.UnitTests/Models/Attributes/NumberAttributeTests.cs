namespace RemoteService.Models.Attributes;

public class NumberAttributeTests {
    private readonly AttributeDefinition _definition;
    private readonly NumberAttribute<int> _attribute;

    public NumberAttributeTests() {
        _definition = new() {
            Name = "TestName",
            Description = "TestDescription",
            DataType = typeof(int),
        };

        _attribute = new() {
            Definition = _definition,
            Value = 42
        };
    }

    [Fact]
    public void Constructor_InitializesProperties() {
        _attribute.Definition.Should().Be(_definition);
        _attribute.Value.Should().Be(42);
        _attribute.Validate().IsSuccess.Should().BeTrue();
    }

    private class TestData : TheoryData<string, object[], bool> {
        public TestData() {
            Add("IsLessThan", new object[] { 99 }, true);
            Add("IsLessThan", new object[] { 2 }, false);
            Add("IsGreaterThan", new object[] { 2 }, true);
            Add("IsGreaterThan", new object[] { 99 }, false);
            Add("IsEqualTo", new object[] { 42 }, true);
            Add("IsEqualTo", new object[] { 13 }, false);
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
        _definition.Constraints.Add(new AttributeConstraint("IsLessThan", "wrong"));

        _attribute.Invoking(x => x.Validate()).Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Validate_WithInvalidNumberOfArguments_ThrowsArgumentException() {
        _definition.Constraints.Add(new AttributeConstraint("IsLessThan"));

        _attribute.Invoking(x => x.Validate()).Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Validate_WithInvalidConstraint_ThrowsArgumentException() {
        _definition.Constraints.Add(new AttributeConstraint("Invalid"));

        _attribute.Invoking(x => x.Validate()).Should().Throw<InvalidOperationException>();
    }
}
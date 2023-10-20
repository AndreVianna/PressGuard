namespace RemoteService.Models.Attributes;

public class DictionaryAttributeTests {
    private readonly AttributeDefinition _definition;
    private readonly DictionaryAttribute<string, int> _attribute;

    public DictionaryAttributeTests() {
        _definition = new() {
            Name = "TestName",
            Description = "TestDescription",
            DataType = typeof(Dictionary<string, int>),
        };

        _attribute = new() {
            Definition = _definition,
            Value = new() { ["TestValue1"] = 1, ["TestValue2"] = 2, ["TestValue3"] = 3 }
        };
    }

    [Fact]
    public void Constructor_InitializesProperties() {
        _attribute.Definition.Should().Be(_definition);
        _attribute.Value.Should().BeEquivalentTo(new Dictionary<string, int> { ["TestValue1"] = 1, ["TestValue2"] = 2, ["TestValue3"] = 3 });
        _attribute.Validate().IsSuccess.Should().BeTrue();
    }

    private class TestData : TheoryData<string, object[], bool> {
        public TestData() {
            Add("Has", new object[] { 3 }, true);
            Add("Has", new object[] { 13 }, false);
            Add("HasAtLeast", new object[] { 1 }, true);
            Add("HasAtLeast", new object[] { 99 }, false);
            Add("HasAtMost", new object[] { 99 }, true);
            Add("HasAtMost", new object[] { 1 }, false);
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
        _definition.Constraints.Add(new AttributeConstraint("Has", "wrong"));

        _attribute.Invoking(x => x.Validate()).Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Validate_WithInvalidNumberOfArguments_ThrowsArgumentException() {
        _definition.Constraints.Add(new AttributeConstraint("Has"));

        _attribute.Invoking(x => x.Validate()).Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Validate_WithInvalidConstraint_ThrowsArgumentException() {
        _definition.Constraints.Add(new AttributeConstraint("Invalid"));

        _attribute.Invoking(x => x.Validate()).Should().Throw<InvalidOperationException>();
    }
}
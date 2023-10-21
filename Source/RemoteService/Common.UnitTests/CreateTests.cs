namespace System;

public class CreateTests {
    // ReSharper disable once ClassNeverInstantiated.Local - Used for testing
    private class TestClass { }

    [Fact]
    public void Instance_NoArgs_CreatesObjectOfType() {
        // Arrange & Act
        var instance = Create.Instance<TestClass>();

        // Assert
        instance.Should().NotBeNull();
        instance.Should().BeOfType<TestClass>();
    }

    // ReSharper disable once ClassNeverInstantiated.Local - Used for testing
    private class TestClassWithArgs {
        public string Value { get; }

        public TestClassWithArgs(string value) {
            Value = value;
        }
    }

    [Fact]
    public void Instance_WithArgs_CreatesObjectOfTypeAndSetsValues() {
        // Arrange
        const string expectedValue = "Test";

        // Act
        var instance = Create.Instance<TestClassWithArgs>(expectedValue);

        // Assert
        instance.Should().NotBeNull();
        instance.Should().BeOfType<TestClassWithArgs>();
        instance.Value.Should().Be(expectedValue);
    }

    [Theory]
    [InlineData("String", typeof(string))]
    [InlineData("Integer", typeof(int))]
    [InlineData("Decimal", typeof(decimal))]
    [InlineData("List<String>", typeof(List<string>))]
    [InlineData("List<Integer>", typeof(List<int>))]
    [InlineData("List<Decimal>", typeof(List<decimal>))]
    [InlineData("Dictionary<Integer,Integer>", typeof(Dictionary<int, int>))]
    [InlineData("Dictionary<Integer,String>", typeof(Dictionary<int, string>))]
    [InlineData("Dictionary<Integer,Decimal>", typeof(Dictionary<int, decimal>))]
    [InlineData("Dictionary<String,Integer>", typeof(Dictionary<string, int>))]
    [InlineData("Dictionary<String,String>", typeof(Dictionary<string, string>))]
    [InlineData("Dictionary<String,Decimal>", typeof(Dictionary<string, decimal>))]
    [InlineData("Dictionary<Decimal,Integer>", typeof(Dictionary<decimal, int>))]
    [InlineData("Dictionary<Decimal,String>", typeof(Dictionary<decimal, string>))]
    [InlineData("Dictionary<Decimal,Decimal>", typeof(Dictionary<decimal, decimal>))]
    [InlineData("Integer[]", typeof(int[]))]
    [InlineData("String[]", typeof(string[]))]
    [InlineData("Decimal[]", typeof(decimal[]))]
    [InlineData("Int64", typeof(long))]
    public void TypeFrom_ReturnsType(string name, Type expectedType) {
        var result = Create.TypeFrom(name);

        result.Should().Be(expectedType);
    }

    [Fact]
    public void TypeFrom_ForInvalidType_Throws() {
        var action = () => Create.TypeFrom("Long");

        action.Should().Throw<InvalidOperationException>().WithMessage("Unsupported type 'Long'.");
    }
}

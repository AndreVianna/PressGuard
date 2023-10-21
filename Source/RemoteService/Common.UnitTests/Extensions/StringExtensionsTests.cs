namespace System.Extensions;

public class StringExtensionsTests {
    [Theory]
    [InlineData("shadow", "Shadow")]
    [InlineData("hello world", "HelloWorld")]
    [InlineData("this is a test", "ThisIsATest")]
    [InlineData("PascalCase", "PascalCase")]
    [InlineData("B i r d", "BIRD")]
    [InlineData("", "")]
    [InlineData("123test", "123Test")]
    [InlineData("test-abc", "TestAbc")]
    public void ToPascalCase_CorrectlyConvertsInput(string input, string expectedResult) {
        // Act
        var result = input.ToPascalCase();

        // Assert
        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData("shadow", "shadow")]
    [InlineData("hello world", "helloWorld")]
    [InlineData("this is a test", "thisIsATest")]
    [InlineData("CamelCase", "camelCase")]
    [InlineData("B i r d", "bIRD")]
    [InlineData("", "")]
    [InlineData("123test", "123Test")]
    [InlineData("test-abc", "testAbc")]
    public void ToCamelCase_CorrectlyConvertsInput(string input, string expectedResult) {
        // Act
        var result = input.ToCamelCase();

        // Assert
        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData("hello world", "HW")]
    [InlineData("this is a test", "TiaT")]
    [InlineData("Acronym", "A")]
    [InlineData("shadow", "S")]
    [InlineData("", "")]
    [InlineData("123test", "1T")]
    [InlineData("test-abc", "TA")]
    public void ToAcronym_CorrectlyConvertsInput(string input, string expectedResult) {
        // Act
        var result = input.ToAcronym();

        // Assert
        result.Should().Be(expectedResult);
    }

    [Fact]
    public void IsRequired_ReturnsConnector() {
        // Arrange
        var subject = "42.0m";

        // Act
        var result = subject.IsRequired();

        // Assert
        result.Should().BeOfType<Connector<string?, StringValidator>>();
    }

    [Fact]
    public void IsRequired_ForNullable_ReturnsConnector() {
        // Arrange
        string? subject = default;

        // Act
        var result = subject.IsRequired();

        // Assert
        result.Should().BeOfType<Connector<string?, StringValidator>>();
    }

    [Fact]
    public void IsOptional_ForNullable_ReturnsConnector() {
        // Arrange
        string? subject = default;

        // Act
        var result = subject.IsOptional();

        // Assert
        result.Should().BeOfType<Connector<string?, StringValidator>>();
    }
}

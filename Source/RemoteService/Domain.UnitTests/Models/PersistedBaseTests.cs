using RemoteService.Constants;

namespace RemoteService.Models;

public class PersistedBaseTests {
    public record TestPersistedBase : PersistedBase;

    [Fact]
    public void Constructor_CreatesObject() {
        var testBase = new TestPersistedBase {
            Name = "TestName",
            Description = "TestDescription",
        };

        testBase.Name.Should().Be("TestName");
        testBase.Description.Should().Be("TestDescription");
    }

    private class TestData : TheoryData<TestPersistedBase, bool, int> {
        public TestData() {
            Add(new() { Name = "TestName", Description = "TestDescription" }, true, 0);
            Add(new() { Name = null!, Description = null! }, false, 2);
            Add(new() { Name = "", Description = "" }, false, 2);
            Add(new() { Name = "  ", Description = "  " }, false, 2);
            Add(new() {
                Name = new('X', Validation.Name.MaximumLength + 1),
                Description = new('X', Validation.Description.MaximumLength + 1),
            }, false, 2);
        }
    }

    [Theory]
    [ClassData(typeof(TestData))]
    public void Validate_Validates(TestPersistedBase subject, bool isSuccess, int errorCount) {
        // Act
        var result = subject.Validate();

        // Assert
        result.IsSuccess.Should().Be(isSuccess);
        result.Errors.Should().HaveCount(errorCount);
    }
}

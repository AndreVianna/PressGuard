using RemoteService.Models;
using RemoteService.Models.Attributes;

namespace RemoteService.Handlers.Setting;

public class SettingTests {
    [Fact]
    public void Constructor_CreatesInstance() {
        var agent = new Setting {
            Name = "TestName",
            Description = "TestDescription"
        };

        agent.Should().NotBeNull();
    }

    [Fact]
    public void Validate_Validates() {
        var attributeDefinitions = new List<AttributeDefinition> {
            new() {
                Name = "TestAttribute1",
                Description = "TestDescription1",
                ShortName = "TA1",
                DataType = typeof(string)
            },
            new() {
                Name = "TestAttribute2",
                Description = "TestDescription2",
                ShortName = "TA2",
                DataType = typeof(int)
            },
        };
        var components = new List<Base> {
            new() {
                Name = "TestComponent1",
                Description = "TestDescription1",
                ShortName = "TC1",
            },
            new() {
                Name = "TestComponent2",
                Description = "TestDescription2",
                ShortName = "TC2",
            },
        };
        var testBase = new Setting {
            Name = "TestName",
            Description = "TestDescription",
            ShortName = "GSS",
            Tags = new[] { "Tag1", "Tag2" },
            AttributeDefinitions = attributeDefinitions,
            Components = components,
        };

        var result = testBase.Validate();

        result.IsSuccess.Should().BeTrue();
        result.Errors.Should().HaveCount(0);
    }

    [Fact]
    public void Validate_WithErrors_Validates() {
        var attributeDefinitions = new List<AttributeDefinition> {
            new() {
                Name = "TestAttribute1",
                Description = null!,
                ShortName = "TA1",
                DataType = typeof(string)
            },
            null!,
        };
        var components = new List<Base> {
            new() {
                Name = null!,
                Description = "TestDescription1",
                ShortName = "TC1",
            },
            null!,
        };
        var subject = new Setting {
            Name = "TestName",
            Description = "TestDescription",
            ShortName = "GSS",
            Tags = new[] { "Tag1", "Tag2" },
            AttributeDefinitions = attributeDefinitions,
            Components = components,
        };

        var result = subject.Validate();

        result.IsSuccess.Should().BeFalse();
        result.Errors.Select(i => i.Message).Should().BeEquivalentTo(
        "'AttributeDefinitions[0].Description' cannot be null.",
        "'AttributeDefinitions[1]' cannot be null.",
        "'Components[0].Name' cannot be null.",
        "'Components[1]' cannot be null.");
    }
}
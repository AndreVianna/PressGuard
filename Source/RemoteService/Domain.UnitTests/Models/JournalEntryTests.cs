namespace RemoteService.Models;

public class JournalEntryTests {
    [Fact]
    public void Constructor_InitializesProperties() {
        var attribute = new JournalEntry {
            Section = "Some Section",
            Title = "Some Title",
            Text = "Some text.",
        };

        attribute.Section.Should().Be("Some Section");
        attribute.Title.Should().Be("Some Title");
        attribute.Text.Should().Be("Some text.");
    }
}
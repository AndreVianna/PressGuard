namespace RemoteService.Models;

public class AgentTests {
    [Fact]
    public void Constructor_CreatesInstance() {
        var agent = new Agent {
            Name = "TestName",
            Description = "TestDescription",
            Inventory = new List<InventoryEntry>(),
            Journal = new List<JournalEntry>(),
        };

        agent.Should().NotBeNull();
        agent.Inventory.Should().BeEmpty();
        agent.Journal.Should().BeEmpty();
    }
}
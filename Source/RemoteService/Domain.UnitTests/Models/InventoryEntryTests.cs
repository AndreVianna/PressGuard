namespace RemoteService.Models;

public class InventoryEntryTests {
    [Fact]
    public void Constructor_InitializesProperties() {
        var item = new GameObject {
            Name = "TestName",
            ShortName = "TST",
            Description = "TestDescription",
            Unit = "Kg"
        };

        var attribute = new InventoryEntry {
            Item = item,
            Quantity = 3.1m,
        };

        attribute.Item.Should().Be(item);
        attribute.Quantity.Should().Be(3.1m);
    }
}
namespace RemoteService.Models;

public record Agent : Component {
    public IList<InventoryEntry> Inventory { get; init; } = new List<InventoryEntry>();
    public IList<JournalEntry> Journal { get; init; } = new List<JournalEntry>();
}
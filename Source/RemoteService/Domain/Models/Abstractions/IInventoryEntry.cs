namespace RemoteService.Models.Abstractions;

public interface IInventoryEntry {
    IGameObject Item { get; }
    decimal Quantity { get; }
}
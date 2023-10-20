using RemoteService.Models.Abstractions;

namespace RemoteService.Models;

public record JournalEntry : IJournalEntry {
    public required string Section { get; init; }
    public required string Title { get; init; }
    public required string Text { get; init; }
}
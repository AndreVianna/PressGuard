namespace RemoteService.Models.Abstractions;

public interface IBase {
    string Name { get; }
    string? ShortName { get; }
    string Description { get; }
    ICollection<string> Tags { get; }
}
namespace RemoteService.Models;

public interface IBase : IValidatable {
    string Name { get; }
    string Description { get; }
}

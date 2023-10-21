namespace RemoteService.Models.Abstractions;

public interface IAttribute : IValidatable {
    AttributeDefinition Definition { get; }
    object? Value { get; }
}

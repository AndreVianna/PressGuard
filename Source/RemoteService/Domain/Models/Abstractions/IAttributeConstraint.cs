namespace RemoteService.Models.Abstractions;

public interface IAttributeConstraint {
    string ValidatorName { get; }
    ICollection<object> Arguments { get; }
}
namespace System.Validation.Commands.Abstractions;

public interface IValidationCommand {
    Result Validate(object? subject);
    Result Negate(object? subject);
}

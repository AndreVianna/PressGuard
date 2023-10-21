namespace System.Validation.Builder.Abstractions;

public interface IValidatableValidator : IValidator {
    IConnector<ValidatableValidator> IsNull();
    IConnector<ValidatableValidator> IsNotNull();
    IConnector<ValidatableValidator> IsValid();
}
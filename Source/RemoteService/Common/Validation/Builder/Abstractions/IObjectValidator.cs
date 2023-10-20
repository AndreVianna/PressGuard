namespace System.Validation.Builder.Abstractions;

public interface IObjectValidator : IValidator {
    IConnector<ObjectValidator> IsNull();
    IConnector<ObjectValidator> IsNotNull();
}
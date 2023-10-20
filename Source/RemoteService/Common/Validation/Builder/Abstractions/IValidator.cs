namespace System.Validation.Builder.Abstractions;

public interface IValidator {
    ValidatorMode Mode { get; }
    string Source { get; }
    Result Result { get; }

    Validator SetMode(ValidatorMode mode);
    void Negate();
    void ClearErrors();
    void AddError(ValidationError error);
    void AddErrors(IEnumerable<ValidationError> errors);
}

namespace System.Validation.Builder.Abstractions;

public interface ITypeValidator : IValidator {
    IConnector<TypeValidator> IsNull();
    IConnector<TypeValidator> IsNotNull();
    IConnector<TypeValidator> IsEqualTo<TType>();
}
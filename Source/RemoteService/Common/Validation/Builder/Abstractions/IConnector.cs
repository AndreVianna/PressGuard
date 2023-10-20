namespace System.Validation.Builder.Abstractions;

public interface IConnector<TValidator>
    : ITerminator,
      IBinaryConnector<TValidator>,
      IBinaryOperator<TValidator>
    where TValidator : IValidator {
}
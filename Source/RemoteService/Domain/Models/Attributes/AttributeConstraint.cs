using System.Validation.Commands;
using System.Validation.Commands.Abstractions;

namespace RemoteService.Models.Attributes;

public sealed record AttributeConstraint : IAttributeConstraint {
    public AttributeConstraint(string validatorName, params object[] arguments) {
        ValidatorName = validatorName;
        Arguments = arguments;
    }
    public string ValidatorName { get; }
    public ICollection<object> Arguments { get; }

    public IValidationCommand Create<TValue>(string definitionName)
        => ValidationCommandFactory
          .For(typeof(TValue), definitionName)
          .Create(ValidatorName, Arguments.ToArray());
}
namespace System.Extensions;

public static class ValidatableExtensions {
    public static IConnector<ValidatableValidator> IsOptional(this IValidatable? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => Create(allowNull: true, subject, source!);
    public static IConnector<ValidatableValidator> IsRequired(this IValidatable? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => Create(allowNull: false, subject, source!);
}
namespace System.Extensions;

public static class IntegerExtensions {
    public static IConnector<IntegerValidator> Is(this int subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => Create(allowNull: false, subject, source!);
    public static IConnector<IntegerValidator> IsOptional(this int? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => Create(allowNull: true, subject, source!);
    public static IConnector<IntegerValidator> IsRequired(this int? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => Create(allowNull: false, subject, source!);
}
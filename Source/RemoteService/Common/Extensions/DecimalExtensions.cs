namespace System.Extensions;

public static class DecimalExtensions {
    public static IConnector<DecimalValidator> Is(this decimal subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => Create(allowNull: false, subject, source!);
    public static IConnector<DecimalValidator> IsOptional(this decimal? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => Create(allowNull: true, subject, source!);
    public static IConnector<DecimalValidator> IsRequired(this decimal? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => Create(allowNull: false, subject, source!);
}
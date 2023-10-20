namespace System.Extensions;

public static class DateTimeExtensions {
    public static IConnector<DateTimeValidator> Is(this DateTime subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => Create(allowNull: false, subject, source!);
    public static IConnector<DateTimeValidator> IsOptional(this DateTime? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => Create(allowNull: true, subject, source!);
    public static IConnector<DateTimeValidator> IsRequired(this DateTime? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => Create(allowNull: false, subject, source!);
}
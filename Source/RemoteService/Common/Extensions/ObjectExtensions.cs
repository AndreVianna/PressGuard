namespace System.Extensions;

public static class ObjectExtensions {
    public static IConnector<ObjectValidator> IsRequired(this object? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => Create(allowNull: false, subject, source!);
    public static IConnector<ObjectValidator> IsOptional(this object? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => Create(allowNull: true, subject, source!);
}

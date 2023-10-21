namespace System.Extensions;

public static class ServiceCollectionExtensions {
    public static IServiceCollection AddSystemUtilities(this IServiceCollection services) {
        services.AddSingleton<DateTimeProvider>();
        services.AddSingleton<FileSystemAccessor>();
        return services;
    }
}

namespace RemoteService.Extensions;

public static class ServiceCollectionExtensions {
    public static IServiceCollection AddRepositories(this IServiceCollection services) {
        services.TryAddScoped(typeof(IJsonFileStorage<>), typeof(JsonFileStorage<>));
        services.TryAddScoped<IUserRepository, UserRepository>();
        services.TryAddScoped<ISystemRepository, SystemRepository>();
        services.TryAddScoped<ISettingRepository, SettingRepository>();
        return services;
    }
}

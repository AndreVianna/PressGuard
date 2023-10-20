using RemoteService.Repositories;
using RemoteService.Repositories.Auth;
using RemoteService.Repositories.Settings;
using RemoteService.Repositories.Systems;

namespace RemoteService.Extensions;

public static class ServiceCollectionExtensions {
    public static IServiceCollection AddRepositories(this IServiceCollection services) {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ISystemRepository, SystemRepository>();
        services.AddScoped<ISettingRepository, SettingRepository>();
        services.AddScoped(typeof(IJsonFileStorage<>), typeof(Repositories.JsonFileStorage<>));
        return services;
    }
}
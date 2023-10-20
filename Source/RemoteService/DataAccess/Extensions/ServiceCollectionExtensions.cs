using DataAccess.Repositories;
using DataAccess.Repositories.Auth;
using DataAccess.Repositories.Settings;
using DataAccess.Repositories.Systems;

namespace DataAccess.Extensions;

public static class ServiceCollectionExtensions {
    public static IServiceCollection AddRepositories(this IServiceCollection services) {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ISystemRepository, SystemRepository>();
        services.AddScoped<ISettingRepository, SettingRepository>();
        services.AddScoped(typeof(IJsonFileStorage<>), typeof(Repositories.JsonFileStorage<>));
        return services;
    }
}
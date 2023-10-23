using RemoteService.Repositories.Tenants;
using RemoteService.Repositories.Venues;

namespace RemoteService.Extensions;

public static class ServiceCollectionExtensions {
    public static IServiceCollection AddRepositories(this IServiceCollection services) {
        services.TryAddScoped(typeof(IJsonFileStorage<>), typeof(JsonFileStorage<>));
        services.TryAddScoped<IUserRepository, UserRepository>();
        services.TryAddScoped<ITenantRepository, TenantRepository>();
        services.TryAddScoped<IVenueRepository, VenueRepository>();
        return services;
    }
}

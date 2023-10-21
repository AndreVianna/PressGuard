namespace RemoteService.Extensions;

public static class ServiceCollectionExtensions {
    public static IServiceCollection AddDomainHandlers<TTokenGenerator>(this IServiceCollection services, IConfiguration configuration)
        where TTokenGenerator : class, ITokenGenerator {
        services.AddTokenGenerator<TTokenGenerator>(configuration);
        services.TryAddScoped<IAuthHandler, AuthHandler>();
        services.TryAddScoped<ISystemHandler, SystemHandler>();
        services.TryAddScoped<ISettingHandler, SettingHandler>();
        return services;
    }

    private static void AddTokenGenerator<TInstance>(this IServiceCollection services, IConfiguration configuration)
        where TInstance : class, ITokenGenerator {
        services.Configure<AuthSettings>(configuration.GetSection("Security"));
        services.TryAddScoped<ITokenGenerator, TInstance>();
    }
}

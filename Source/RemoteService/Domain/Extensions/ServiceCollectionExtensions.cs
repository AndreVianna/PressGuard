using RemoteService.Authentication;
using RemoteService.Handlers.Auth;
using RemoteService.Handlers.Setting;
using RemoteService.Handlers.System;

namespace RemoteService.Extensions;

public static class ServiceCollectionExtensions {
    public static IServiceCollection AddTokenGenerator<TInstance>(this IServiceCollection services, IConfiguration configuration)
        where TInstance : class, ITokenGenerator {
        services.Configure<AuthSettings>(configuration.GetSection("Security"));
        services.AddScoped<ITokenGenerator, TInstance>();
        return services;
    }

    public static IServiceCollection AddDomainHandlers<TTokenGenerator>(this IServiceCollection services, IConfiguration configuration)
        where TTokenGenerator : class, ITokenGenerator {
        services.AddTokenGenerator<TTokenGenerator>(configuration);
        services.AddScoped<IAuthHandler, AuthHandler>();
        services.AddScoped<ISystemHandler, SystemHandler>();
        services.AddScoped<ISettingHandler, SettingHandler>();
        return services;
    }

}

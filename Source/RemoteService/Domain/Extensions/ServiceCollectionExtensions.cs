using RemoteService.Handlers.Auth;
using RemoteService.Handlers.Setting;
using RemoteService.Handlers.System;

namespace RemoteService.Extensions;

public static class ServiceCollectionExtensions {
    public static IServiceCollection AddDomainHandlers(this IServiceCollection services, IConfiguration configuration) {
        services.Configure<AuthSettings>(configuration.GetSection("Security"));

        services.AddScoped<ITokenGenerator, TokenGenerator>();
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<IAuthHandler, AuthHandler>();
        services.AddScoped<ISystemHandler, SystemHandler>();
        services.AddScoped<ISettingHandler, SettingHandler>();
        return services;
    }
}

using Auth.Interfaces;
using Auth.services;
using Microsoft.Extensions.DependencyInjection;

namespace Auth;

public static class StartupConfigurator {
    public static void AddTokenService(this IServiceCollection services) {
        services.AddScoped<ITokenService, TokenService>();
    }
}
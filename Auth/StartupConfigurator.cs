using Microsoft.Extensions.DependencyInjection;
using Repository.Interfaces;
using Repository.services.auth;

namespace Auth;

public static class StartupConfigurator {
    public static void AddTokenService(this IServiceCollection services) {
        services.AddScoped<ITokenService, TokenService>();
    }
}
using Microsoft.Extensions.DependencyInjection;
using Repository.Services.Auth;

namespace Auth;

public static class StartupConfigurator {
    public static void AddTokenService(this IServiceCollection services) {
        services.AddScoped<ITokenService, TokenService>();
    }
}
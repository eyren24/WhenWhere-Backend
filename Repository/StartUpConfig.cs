using Microsoft.Extensions.DependencyInjection;
using Repository.Interfaces;
using Repository.services.auth;

namespace Repository
{
    public static class StartUpConfig
    {
        public static void AddServiceDB(this IServiceCollection services)
        {
            services.AddTransient<ITokenService, TokenService>();
            /*services.AddTransient<>();
            services.AddTransient<>();*/
        }
    }
}

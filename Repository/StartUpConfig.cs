using Microsoft.Extensions.DependencyInjection;
using Repository.Services.Auth;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

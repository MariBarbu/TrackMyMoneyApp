using Microsoft.Extensions.DependencyInjection;
using DataLayer;
using DataLayer.Repositories;

namespace WebApi.Helpers
{
    public static class Services
    {

        public static void AddServices(this IServiceCollection services)
        {
           
            //services.AddScoped<IAuthentificationService, AuthentificationService>();

        }



        public static void AddRepositories(this IServiceCollection services)
        {
            //services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddScoped<ITokenRepository, TokenRepository>();


        }
    }
}
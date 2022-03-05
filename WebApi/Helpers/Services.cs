using Microsoft.Extensions.DependencyInjection;
using DataLayer;
using DataLayer.Repositories;
using Services;

namespace WebApi.Helpers
{
    public static class Services
    {

        public static void AddServices(this IServiceCollection services)
        {
           
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IMoneyUserService, MoneyUserService>();
            services.AddScoped<IUserAuthentificationHelper, UserAuthentificationHelper>();

        }



        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<IMoneyUserRepository, MoneyUserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenRepository, TokenRepository>();


        }
    }
}
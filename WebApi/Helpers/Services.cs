using Microsoft.Extensions.DependencyInjection;
using DataLayer;
using DataLayer.Repositories;
using Services;
using Jobs;

namespace WebApi.Helpers
{
    public static class Services
    {
        public static void AddJobs(this IServiceCollection services)
        {
            services.AddScoped<IMonthJob, MonthJob>();
        }

        public static void AddServices(this IServiceCollection services)
        {
           
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IMoneyUserService, MoneyUserService>();
            services.AddScoped<IUserAuthenticationHelper, UserAuthenticationHelper>();
            services.AddScoped<IWishService, WishService>();
            services.AddScoped<IMonthService, MonthService>();
            services.AddScoped<ISpendingService, SpendingService>();
            services.AddScoped<ICategoryService, CategoryService>();

        }



        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<IMoneyUserRepository, MoneyUserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<IWishRepository, WishRepository>();
            services.AddScoped<IMonthRepository, MonthRepository>();
            services.AddScoped<ISpendingRepository, SpendingRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();


        }
    }
}
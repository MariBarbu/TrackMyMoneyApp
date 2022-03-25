using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using XamarinApp.Services;
using XamarinApp.ViewModels.Authentication;
using XamarinApp.ViewModels.Wishes;

namespace XamarinApp
{
    public static class Startup
    {
        private static IServiceProvider serviceProvider;
        public static void ConfigureServices()
        {
            var services = new ServiceCollection();

            //add services
            services.AddHttpClient<IWishService, WishService>(c =>
            {
                SetHttpClient(c);
            });
            services.AddHttpClient<IAuthService, AuthService>(c =>
            {
                SetHttpClient(c);
            });

            //add viewmodels
            services.AddTransient<WishesViewModel>();
            services.AddTransient<AddWishViewModel>();
            services.AddTransient<RegisterViewModel>();

            //services.AddTransient<BookDetailsViewModel>();

            serviceProvider = services.BuildServiceProvider();
        }

        private static void SetHttpClient(HttpClient c)
        {
            c.BaseAddress = new Uri("http://192.168.0.104:5000/api/");
            c.DefaultRequestHeaders.Add("Accept", "application/json");
        }
        public static T Resolve<T>() => serviceProvider.GetService<T>();
    }
}
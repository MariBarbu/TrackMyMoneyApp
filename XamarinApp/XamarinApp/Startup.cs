using Microsoft.Extensions.DependencyInjection;
using System;
using XamarinApp.Services;
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
                c.BaseAddress = new Uri("http://localhost:5000/api/");
                c.Timeout = TimeSpan.FromSeconds(120);
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            //add viewmodels
            services.AddTransient<WishesViewModel>();
            //services.AddTransient<AddBookViewModel>();
            //services.AddTransient<BookDetailsViewModel>();

            serviceProvider = services.BuildServiceProvider();
        }

        public static T Resolve<T>() => serviceProvider.GetService<T>();
    }
}
using Microsoft.Extensions.DependencyInjection;
using MoneyXamarin.Services;
using System;

namespace MoneyXamarin
{
    public static class Startup
    {
        private static IServiceProvider serviceProvider;
        public static void ConfigureServices()
        {
            var services = new ServiceCollection();

            //add services
            services.AddHttpClient<IWishService, ApiWishService>(c =>
            {
                c.BaseAddress = new Uri("http://localhost:5000");
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            //add viewmodels
            //services.AddTransient<BooksViewModel>();
            //services.AddTransient<AddBookViewModel>();
            //services.AddTransient<BookDetailsViewModel>();

            serviceProvider = services.BuildServiceProvider();
        }

        public static T Resolve<T>() => serviceProvider.GetService<T>();
    }
}

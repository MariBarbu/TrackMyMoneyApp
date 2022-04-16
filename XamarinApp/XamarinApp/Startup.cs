﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Xamarin.Forms;
using XamarinApp.Helpers;
using XamarinApp.Services;
using XamarinApp.ViewModels.Authentication;
using XamarinApp.ViewModels.Categories;
using XamarinApp.ViewModels.Month;
using XamarinApp.ViewModels.Spendings;
using XamarinApp.ViewModels.Wishes;
using XamarinApp.Views;
using XamarinApp.Views.Authentication;
using XamarinApp.Views.Categories;
using XamarinApp.Views.Month;
using XamarinApp.Views.Spendings;
using XamarinApp.Views.Wishes;

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
            services.AddHttpClient<ICategoryService, CategoryService>(c =>
            {
                SetHttpClient(c);
            });
            services.AddHttpClient<ISpendingService, SpendingService>(c =>
            {
                SetHttpClient(c);
            });
            services.AddHttpClient<IMonthService, MonthService>(c =>
            {
                SetHttpClient(c);
            });

            //add viewmodels
            services.AddTransient<WishesViewModel>();
            services.AddTransient<AddWishViewModel>();
            services.AddTransient<RegisterViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<CategoriesViewModel>();
            services.AddTransient<AddCategoryViewModel>();
            services.AddTransient<SpendingsViewModel>();
            services.AddTransient<AddSpendingViewModel>();
            services.AddTransient<UpdateBudgetViewModel>();
            services.AddTransient<DefaultScreenViewModel>();



            serviceProvider = services.BuildServiceProvider();

            AddRoutes();

            
        }

        private static void AddRoutes()
        {
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(AddWishPage), typeof(AddWishPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(WishesPage), typeof(WishesPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(StartPage), typeof(StartPage));
            Routing.RegisterRoute(nameof(CategoriesPage), typeof(CategoriesPage));
            Routing.RegisterRoute(nameof(AddCategoryPage), typeof(AddCategoryPage));
            Routing.RegisterRoute(nameof(SpendingsPage), typeof(SpendingsPage));
            Routing.RegisterRoute(nameof(AddSpendingPage), typeof(AddSpendingPage));
            Routing.RegisterRoute(nameof(UpdateBudgetPage), typeof(UpdateBudgetPage));
            Routing.RegisterRoute(nameof(DefaultScreenPage), typeof(DefaultScreenPage));

        }
        private static void SetHttpClient(HttpClient c)
        {
            c.BaseAddress = new Uri("http://192.168.1.5:5000/api/");
            c.DefaultRequestHeaders.Add("Accept", "application/json");
            c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);
        }
        public static T Resolve<T>() => serviceProvider.GetService<T>();
    }
}
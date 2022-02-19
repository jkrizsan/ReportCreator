using System;
using DataFormatSwitcher.Data;
using DataFormatSwitcher.Interfaces;
using DataFormatSwitcher.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
namespace DataFormatSwitcher
{
    /// <summary>
    /// Provides Microsoft.Extensions.DependencyInjection.ServiceCollection
    /// </summary>
    public class ServiceProviderFactory
    {
        static IServiceProvider serviceProvider;

        static  ServiceProviderFactory()
        {
            IServiceCollection services = new ServiceCollection();

            ConfigureServices(services);

            serviceProvider = services.BuildServiceProvider();
        }

        static void ConfigureServices(IServiceCollection services)
        {
            IConfiguration config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", true, true)
               .Build();

            services.AddSingleton(config.GetSection("Settings").Get<AppSettings>());

            services.AddLogging(builder =>
                builder.AddSimpleConsole());

            services.AddTransient(typeof(IConverterService), typeof(ConverterService));

            services.AddTransient(typeof(IUserInterfaceService), typeof(UserInterfaceService));
        }

        public static  T GetService<T>() where T : class
            => (T)serviceProvider.GetService(typeof(T));
    }
}

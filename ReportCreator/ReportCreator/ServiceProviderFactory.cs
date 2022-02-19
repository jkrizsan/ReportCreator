using System;
using ReportCreator.BusinessLogic.Data;
using ReportCreator.BusinessLogic.Interfaces;
using ReportCreator.BusinessLogic.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ReportCreator.BusinessLogic.Wrappers;

namespace ReportCreator
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

            services.AddTransient(typeof(IFileWrapper), typeof(FileWrapper));

        }

        public static  T GetService<T>() where T : class
            => (T)serviceProvider.GetService(typeof(T));
    }
}

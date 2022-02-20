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
        private static IServiceProvider _serviceProvider;

        static ServiceProviderFactory()
        {
            IServiceCollection services = new ServiceCollection();

            ConfigureServices(services);

            _serviceProvider = services.BuildServiceProvider();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            IConfiguration config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", true, true)
               .Build();

            services.AddSingleton(config.GetSection("Settings").Get<AppSettings>());

            services.AddLogging(builder =>
                builder.AddSimpleConsole());

            services.AddTransient(typeof(IReportService), typeof(ReportService));

            services.AddTransient(typeof(IUserInterfaceService), typeof(UserInterfaceService));

            services.AddTransient(typeof(IFileWrapper), typeof(FileWrapper));

        }

        public static  T GetService<T>() where T : class
            => (T)_serviceProvider.GetService(typeof(T));
    }
}

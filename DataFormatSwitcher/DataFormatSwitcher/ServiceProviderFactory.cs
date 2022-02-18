using DataFormatSwitcher.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

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
            services.AddLogging(builder =>
                builder.AddSimpleConsole());

            services.AddTransient(typeof(IConverterService), typeof(ConverterService));
        }

        public static  T GetService<T>() where T : class
            => (T)serviceProvider.GetService(typeof(T));
    }
}

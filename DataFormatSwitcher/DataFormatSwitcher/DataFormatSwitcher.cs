using Microsoft.Extensions.Logging;
using DataFormatSwitcher.Interfaces;

namespace DataFormatSwitcher
{
    internal class DataFormatSwitcher
    {
        private static  ILogger _logger;

        private static IConverterService _converterService;

        static void Main(string[] args)
        {
              init();
            _converterService.Convert();
        }

        static void init()
        {
            _logger = ServiceProviderFactory.GetService<ILoggerFactory>()
                .CreateLogger<DataFormatSwitcher>();

            _converterService = ServiceProviderFactory.GetService<IConverterService>();

            _logger.LogInformation("Initialized");
        }

     
    }
}

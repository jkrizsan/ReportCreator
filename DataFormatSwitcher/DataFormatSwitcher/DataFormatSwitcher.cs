using System;
using Microsoft.Extensions.Logging;
using DataFormatSwitcher.Interfaces;
using DataFormatSwitcher.Data;
using DataFormatSwitcher.Exceptions;
using System.Linq;

namespace DataFormatSwitcher
{
    internal class DataFormatSwitcher
    {
        private static ILogger _logger;

        private static IConverterService _converterService;

        private static IUserInterfaceService _userInterfaceService;

        static void Main(string[] args)
        {
            try
            {
                init();
                
                _userInterfaceService.HelpUser(args);
                
                ConvertRequest request = _userInterfaceService.BuildRequest(args);
                
                var rawData = _converterService.ReadDataFromFile(request);
                var data = _converterService.ParseData(rawData.ToList(), request);
                
                _converterService.ConvertTo();
            }
            catch (FileValidationException ex)
            {
                _logger.LogError($"Error message: {ex.ErrorMessage}");
            }
            catch (UserException ex)
            {
                _logger.LogError($"Error message: {ex.ErrorMessage}");
                _logger.LogInformation($"Use -h flag for get help");
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error happened, message: {ex.Message}");
            }

            _logger.LogInformation("Application is finished the run");
        }

        private static void init()
        {
            _logger = ServiceProviderFactory.GetService<ILoggerFactory>()
                .CreateLogger<DataFormatSwitcher>();

            _converterService = ServiceProviderFactory.GetService<IConverterService>();

            _userInterfaceService = ServiceProviderFactory.GetService<IUserInterfaceService>();

            _logger.LogInformation("Application Initialized");
        }

     
    }
}

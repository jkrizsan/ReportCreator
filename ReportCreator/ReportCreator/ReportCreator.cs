using System;
using Microsoft.Extensions.Logging;
using ReportCreator.BusinessLogic.Interfaces;
using ReportCreator.BusinessLogic.Data;
using ReportCreator.BusinessLogic.Exceptions;
using System.Linq;

namespace ReportCreator
{
    internal class ReportCreator
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
                
                var reports = _converterService.CreateReport(data.ToList()).ToList();

                _converterService.CreateReportFile(reports, request);
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
                .CreateLogger<ReportCreator>();

            _converterService = ServiceProviderFactory.GetService<IConverterService>();

            _userInterfaceService = ServiceProviderFactory.GetService<IUserInterfaceService>();

            _logger.LogInformation("Application Initialized");
        }

     
    }
}

using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using ReportCreator.BusinessLogic.Data;
using ReportCreator.BusinessLogic.Interfaces;
using ReportCreator.BusinessLogic.Exceptions;

namespace ReportCreator
{
    internal class ReportCreator
    {
        private static ILogger _logger;

        private static IReportService _converterService;

        private static IUserInterfaceService _userInterfaceService;

        /// <summary>
        /// Entry point of the Application
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            try
            {
                init();
                
                _userInterfaceService.HelpUser(args);
                
                ConvertRequest request = _userInterfaceService.BuildRequest(args);
                
                var rawData = _converterService.ReadDataFromFile(request);

                var parsedData = _converterService.ParseData(rawData, request);
                
                var reports = _converterService.CreateReportData(parsedData).ToList();

                _converterService.CreateReportFile(reports, request);
            }
            catch (UserException ex)
            {
                _logger.LogError($"Error message: {ex.ErrorMessage}");
                _logger.LogInformation($"Use -h flag for get help");
            }
            catch (FileValidationException ex)
            {
                _logger.LogError($"Error message: {ex.ErrorMessage}");
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error happened, message: {ex.Message}");
            }

            _logger.LogInformation("Application is finished the run");
        }

        private static void init()
        {
            _logger = ServiceProviderFactory
                .GetService<ILoggerFactory>()
                .CreateLogger<ReportCreator>();

            _converterService = ServiceProviderFactory.GetService<IReportService>();

            _userInterfaceService = ServiceProviderFactory.GetService<IUserInterfaceService>();

            _logger.LogInformation("Application Initialized");
        }
    }
}

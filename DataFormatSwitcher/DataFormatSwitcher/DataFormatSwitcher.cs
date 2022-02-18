using System;
using Microsoft.Extensions.Logging;
using DataFormatSwitcher.Interfaces;
using DataFormatSwitcher.Data;
using DataFormatSwitcher.Exceptions;

namespace DataFormatSwitcher
{
    internal class DataFormatSwitcher
    {
        private static  ILogger _logger;

        private static IConverterService _converterService;

        static void Main(string[] args)
        {
            try
            {
                init();
                help(args);
                ConvertRequest request = buildRequest(args);
                _converterService.Convert();
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

        private static void help(string[] args)
        {
            if (args[0].ToLowerInvariant().Equals("-h"))
            {
                string helperText = "Arguments which have to be specified for the proper running: \n\n" +
                    "0: Separator character – indicates the separator\n" +
                    "1: InputRegion         – indicates the locale used for parsing numbers and dates\n" +
                    "2: OutputRegion        – indicates the outputlocale used for parsing numbers and dates\n" +
                    "3: FilePath            – indicates the name of the file to read";

                Console.WriteLine();
                Console.WriteLine(helperText);
                Environment.Exit(-1);
            }
        }

        private static ConvertRequest buildRequest(string[] args)
        {
            ConvertRequest request = new ConvertRequest();

            validateAndSetSeparator(args, request);
            validateAndSetInputRegion(args, request);
            validateAndSetOutputRegion(args, request);
            validateAndSetFilePath(args, request);

            return request;
        }

        private static void validateAndSetFilePath(string[] args, ConvertRequest request)
        {
            if (args[3].Length < 2)
            {
                throw new UserException("The fourth argument have to be set!");
            }

            request.FilePath = args[3];
        }

        private static void validateAndSetOutputRegion(string[] args, ConvertRequest request)
        {
            if (args[2].Length < 2)
            {
                throw new UserException("The third argument have to be set!");
            }

            request.OutputRegion = args[2];
        }

        private static void validateAndSetInputRegion(string[] args, ConvertRequest request)
        {
            if (args[1].Length < 2)
            {
                throw new UserException("The second argument have to be set!");
            }

            request.InputRegion = args[1];
        }

        private static void validateAndSetSeparator(string[] args, ConvertRequest request)
        {
            if (args[0].Length != 1)
            {
                throw new UserException("The first argument have to be a single separator character!");
            }

            request.Separator = Convert.ToChar(args[0]);
        }

        private static void init()
        {
            _logger = ServiceProviderFactory.GetService<ILoggerFactory>()
                .CreateLogger<DataFormatSwitcher>();

            _converterService = ServiceProviderFactory.GetService<IConverterService>();

            _logger.LogInformation("Application Initialized");
        }

     
    }
}

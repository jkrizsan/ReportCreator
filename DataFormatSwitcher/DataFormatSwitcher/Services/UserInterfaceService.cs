using DataFormatSwitcher.Data;
using DataFormatSwitcher.Exceptions;
using DataFormatSwitcher.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;

namespace DataFormatSwitcher.Services
{
    public class UserInterfaceService : IUserInterfaceService
    {
        private readonly ILogger _logger;

        private readonly AppSettings _appSettings;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="appSettings"></param>
        public UserInterfaceService(ILoggerFactory loggerFactory, AppSettings appSettings)
        {
            _logger = loggerFactory.CreateLogger<UserInterfaceService>();
            _appSettings = appSettings;
        }

        /// <inheritdoc />
        public ConvertRequest BuildRequest(string[] args)
        {
            _logger.LogInformation("Started to validate the arguments and build convert request based on it.");

            ConvertRequest request = new ConvertRequest();

            validateAndSetSeparator(args, request);
            validateAndSetInputRegion(args, request);
            validateAndSetOutputRegion(args, request);
            validateAndSetFilePath(args, request);

            _logger.LogInformation("Convert request built succesfully!");

            return request;
        }

        /// <inheritdoc />
        public void HelpUser(string[] args)
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

        private void validateAndSetFilePath(string[] args, ConvertRequest request)
        {
            string filePath = args[3];

            if (filePath.Length < 4 || filePath.EndsWith(".csv") == false)
            {
                throw new UserException("The fourth argument have to be set! Supported file format is CSV!");
            }

            if (File.Exists(filePath) == false)
            {
                throw new UserException($"The set '{filePath}' file does not exist!");
            } 

            request.FilePath = filePath;
        }

        private void validateAndSetOutputRegion(string[] args, ConvertRequest request)
        {
            string outputRegion = args[2].ToLowerInvariant();

            if (outputRegion.Length < 2)
            {
                throw new UserException("The third argument have to be set!");
            }

            if (_appSettings.SupportedLanguages.Select(x => x.Name).Contains(outputRegion) == false)
            {
                throw new UserException($"The set '{outputRegion}' output region is a not supported language type");
            }

            request.OutputRegion = outputRegion;
        }

        private void validateAndSetInputRegion(string[] args, ConvertRequest request)
        {
            string inputRegion = args[1].ToLowerInvariant();

            if (inputRegion.Length < 2)
            {
                throw new UserException("The second argument have to be set!");
            }

            if (_appSettings.SupportedLanguages.Select(x => x.Name).Contains(inputRegion) == false)
            {
                throw new UserException($"The set '{inputRegion}' input region is a not supported language type");
            }

            request.InputRegion = inputRegion;
        }

        private void validateAndSetSeparator(string[] args, ConvertRequest request)
        {
            if (args[0].Length != 1)
            {
                throw new UserException("The first argument have to be a single separator character!");
            }

            request.Separator = Convert.ToChar(args[0]);
        }
    }
}

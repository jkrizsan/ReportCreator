using DataFormatSwitcher.Interfaces;
using Microsoft.Extensions.Logging;

namespace DataFormatSwitcher
{
    /// <summary>
    /// ConverterService Class
    /// </summary>
    public class ConverterService : IConverterService
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="loggerFactory"></param>
        public ConverterService(ILoggerFactory loggerFactory)
        {
           _logger = loggerFactory.CreateLogger<ConverterService>();
        }

        /// <inheritdoc />
        public void Convert()
        {
        }
    }
}

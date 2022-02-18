using System;

namespace DataFormatSwitcher.Exceptions
{
    /// <summary>
    /// Indicates exception for file content errors
    /// </summary>
    public class FileValidationException : Exception
    {
        public FileValidationException(string message, int lineNumber)
        {
            ErrorMessage = message;
            LineNumber = lineNumber;
        }

        /// <summary>
        /// Error message
        /// </summary>
        public string ErrorMessage;

        /// <summary>
        /// Line Number
        /// </summary>
        public int LineNumber { get; set; }
    }
}

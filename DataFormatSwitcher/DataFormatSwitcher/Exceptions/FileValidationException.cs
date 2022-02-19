using System;

namespace DataFormatSwitcher.Exceptions
{
    /// <summary>
    /// Indicates exception for file content errors
    /// </summary>
    public class FileValidationException : Exception
    {
        public FileValidationException(string message)
        {
            ErrorMessage = message;
        }

        /// <summary>
        /// Error message
        /// </summary>
        public string ErrorMessage;

    }
}

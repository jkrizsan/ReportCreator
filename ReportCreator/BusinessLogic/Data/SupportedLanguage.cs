namespace ReportCreator.BusinessLogic.Data
{
    /// <summary>
    /// Supported Language types
    /// </summary>
    public class SupportedLanguage
    {
        /// <summary>
        /// Name of the SupportedLanguage, like en or fi
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Culture Info of the SupportedLanguage, like en-GB or fi-FI
        /// </summary>
        public string CultureInfo { get; set; }

        /// <summary>
        /// Date format
        /// </summary>
        public string DateFormat { get; set; }

        /// <summary>
        /// Double Separator, dot or comma
        /// </summary>
        public char DoubleSeparator { get; set; }
    }
}
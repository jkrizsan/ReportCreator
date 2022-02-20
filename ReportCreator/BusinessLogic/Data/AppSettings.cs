using System.Collections.Generic;

namespace ReportCreator.BusinessLogic.Data
{
    /// <summary>
    /// Dto for Application Settings
    /// </summary>
    public  class AppSettings
    {
        /// <summary>
        /// Collection of the Supported Languages
        /// </summary>
        public List<SupportedLanguage> SupportedLanguages { get; set; }

        /// <summary>
        /// Name of the Output/Report CSV file
        /// </summary>
        public string ReportFileName { get; set; }
    }
}

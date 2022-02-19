using System.Collections.Generic;

namespace ReportCreator.Data
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
    }
}

namespace ReportCreator.BusinessLogic.Data
{
    /// <summary>
    /// Dto class for the input/request data
    /// </summary>
    public class ConvertRequest
    {
        /// <summary>
        /// Separator/Delimiter of the file data
        /// </summary>
        public char Separator { get; set; }

        /// <summary>
        /// Short form of the input region language
        /// </summary>
        public string InputRegion { get; set; }

        /// <summary>
        /// Short form of the output region language
        /// </summary>
        public string OutputRegion { get; set; }

        /// <summary>
        /// Input file location
        /// </summary>
        public string FilePath { get; set; }
    }
}

namespace DataFormatSwitcher.Data
{
    /// <summary>
    /// Dto class for the input/request data
    /// </summary>
    public class ConvertRequest
    {
        /// <summary>
        /// Separator of the file data
        /// </summary>
        public char Separator { get; set; }

        /// <summary>
        /// Short form of input region language style
        /// </summary>
        public string InputRegion { get; set; }

        /// <summary>
        /// Short form of output region language style
        /// </summary>
        public string OutputRegion { get; set; }

        /// <summary>
        /// Input file location
        /// </summary>
        public string FilePath { get; set; }
    }
}

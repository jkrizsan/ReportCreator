using DataFormatSwitcher.Data;
using System.Collections.Generic;

namespace DataFormatSwitcher.Interfaces
{
    /// <summary>
    /// Converter Interface
    /// </summary>
    public  interface IConverterService
    {
        /// <summary>
        /// Read up the set file and convert the content to the RawFileData items
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Collection of RawFileData</returns>
        IEnumerable<RawFileData> ReadDataFromFile(ConvertRequest request);

        /// <summary>
        /// Convert raw data to another data type which one easier to process further
        /// </summary>
        /// <param name="rawData"></param>
        /// <param name="request"></param>
        /// <returns>Collection of FileData</returns>
        IEnumerable<FileData> ParseData(List<RawFileData> rawData, ConvertRequest request);

        void ConvertTo();
    }
}

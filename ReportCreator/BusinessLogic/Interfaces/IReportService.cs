using System.Collections.Generic;
using ReportCreator.BusinessLogic.Data;

namespace ReportCreator.BusinessLogic.Interfaces
{
    /// <summary>
    /// Interface of the Report Service
    /// </summary>
    public interface IReportService
    {
        /// <summary>
        /// Read up the given file and convert the content to the RawFileData items
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Collection of RawFileData</returns>
        List<RawFileData> ReadDataFromFile(ConvertRequest request);

        /// <summary>
        /// Create the output/report file
        /// </summary>
        /// <param name="reportData"></param>
        /// <param name="request"></param>
        void CreateReportFile(IEnumerable<ReportData> reportData, ConvertRequest request);

        /// <summary>
        /// Convert raw data to another data type which one easier to process further
        /// </summary>
        /// <param name="rawData"></param>
        /// <param name="request"></param>
        /// <returns>Collection of FileData</returns>
        List<FileData> ParseData(List<RawFileData> rawData, ConvertRequest request);

        /// <summary>
        /// Create output/report objects based on the file data
        /// </summary>
        /// <param name="fileData"></param>
        /// <returns>Collection of ReportData</returns>
        IEnumerable<ReportData> CreateReportData(List<FileData> fileData);
    }
}

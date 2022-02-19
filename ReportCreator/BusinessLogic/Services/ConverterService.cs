using CsvHelper;
using CsvHelper.Configuration;
using ReportCreator.BusinessLogic.Data;
using ReportCreator.BusinessLogic.Exceptions;
using ReportCreator.BusinessLogic.Interfaces;
using ReportCreator.Mappers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace ReportCreator.BusinessLogic.Services
{
    /// <summary>
    /// ConverterService Class
    /// </summary>
    public class ConverterService : IConverterService
    {
        private readonly ILogger _logger;

        private readonly AppSettings _appSettings;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="loggerFactory"></param>
        public ConverterService(ILoggerFactory loggerFactory, AppSettings appSettings)
        {
            _logger = loggerFactory.CreateLogger<ConverterService>();
            _appSettings = appSettings;
        }

        /// <inheritdoc />
        public IEnumerable<FileData> ParseData(List<RawFileData> rawData, ConvertRequest request)
        {
            List<FileData> data = new List<FileData>();

            _logger.LogInformation("Started to parse data from raw format.");

            for (int i = 0; i < rawData.Count; i++)
            {
                var fileData = new FileData();

                mapIntData(rawData, fileData, i);
                mapDoubleData(request, rawData, fileData, i);
                mapDateTimeData(request, rawData, fileData, i);
                mapStringData(rawData[i], fileData);

                data.Add(fileData);
            }

            _logger.LogInformation("Raw data parsing is done.");

            return data;
        }

        /// <inheritdoc />
        public IEnumerable<RawFileData> ReadDataFromFile(ConvertRequest request)
        {
            _logger.LogInformation("Started to read data from the file.");

            IEnumerable<RawFileData> records;

            var lan = getCurrentSupportedLanguageByName(request.OutputRegion);

            var config = new CsvConfiguration(new CultureInfo(lan.CultureInfo))
            {
                Delimiter = request.Separator.ToString(),
            };

            using (var reader = new StreamReader(request.FilePath))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Context.RegisterClassMap<RawFileDataCSVMap>();
                records = csv.GetRecords<RawFileData>().ToList();
            }

            _logger.LogInformation("Data reading from the file was successful.");

            return records;
        }

        /// <inheritdoc />
        public void CreateReportFile(IEnumerable<ReportData> reportData, ConvertRequest request)
        {
            _logger.LogInformation("Started to create report file.");

            FileInfo reportFile = new FileInfo(request.FilePath);

            var lan = getCurrentSupportedLanguageByName(request.OutputRegion);

            var config = new CsvConfiguration(new CultureInfo(lan.CultureInfo))
            {
                Delimiter = request.Separator.ToString()
            };

            using (var writer = new StreamWriter($"{reportFile.Directory}\\Outputdata.csv"))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.Context.RegisterClassMap<ReportDataCSVMap>();
                csv.WriteRecords(reportData);
            }

            _logger.LogInformation("Report file created successful.");
        }

        /// <inheritdoc />
        public IEnumerable<ReportData> CreateReport(List<FileData> fileData)
            => fileData.GroupBy(x => x.Region)
                .Select(x => new ReportData()
                {
                    Region = x.Key,
                    LastOrderDate = x.OrderByDescending(y => y.OrderDate).FirstOrDefault().OrderDate,
                    TotalUnits = x.Sum(y => y.Units),
                    TotalCost = Math.Round(x.Sum(y => y.Units * y.UnitCost), 2),
                });
        

        private void mapStringData(RawFileData rawFileData, FileData data)
        {
            data.Region = rawFileData.Region;
            data.Item = rawFileData.Item;
            data.Rep = rawFileData.Rep;
        }

        private void mapIntData(List<RawFileData> rawData, FileData fileData, int i)
        {
            int number;

            if (int.TryParse(rawData[i].Units, out number))
            {
                fileData.Units = number;
                return;
            }

            throw new FileValidationException($"In the line {i + 1}, the {nameof(RawFileData.Units)} value is not valid");
        }

        private void mapDoubleData(ConvertRequest request, List<RawFileData> rawData, FileData fileData, int i)
        {
            double number;
            string stringValue = rawData[i].UnitCost;

            char inputSeparator = getCurrentSupportedLanguageByName(request.InputRegion)
                .DoubleSeparator;

            stringValue = stringValue.Replace(inputSeparator, ',');

            if (double.TryParse(stringValue, out number))
            {
                fileData.UnitCost = number;
                return;
            }

            throw new FileValidationException($"In the line {i + 1}, the {nameof(RawFileData.UnitCost)} value is not valid");
        }

        private void mapDateTimeData(ConvertRequest request, List<RawFileData> rawData, FileData fileData, int i)
        {
            string stringValue = rawData[i].OrderDate;

            var supLan = getCurrentSupportedLanguageByName(request.InputRegion);

            try
            {
                fileData.OrderDate = DateTime.ParseExact(stringValue, supLan.DateFormat, new CultureInfo(supLan.CultureInfo));
            }
            catch (Exception ex)
            {
                throw new FileValidationException($"In the line {i + 1}, the {nameof(RawFileData.OrderDate)} value is not valid." +
                    $" Error message: {ex.Message}");
            }
        }

        private SupportedLanguage getCurrentSupportedLanguageByName(string name)
            => _appSettings.SupportedLanguages
               .Where(x => x.Name.Equals(name))
               .FirstOrDefault();
    }
}


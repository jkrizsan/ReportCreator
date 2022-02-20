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
    /// ReportService Class
    /// </summary>
    public class ReportService : IReportService
    {
        private readonly ILogger _logger;

        private readonly AppSettings _appSettings;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="loggerFactory"></param>
        public ReportService(ILoggerFactory loggerFactory, AppSettings appSettings)
        {
            _logger = loggerFactory.CreateLogger<ReportService>();
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
            _logger.LogInformation("Started to load data from the file.");

            IEnumerable<RawFileData> records;

            var config = getCsvConfiguration(request.InputRegion, request.Separator);

            using (var reader = new StreamReader(request.FilePath))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Context.RegisterClassMap<RawFileDataCSVMap>();
                records = csv.GetRecords<RawFileData>().ToList();
            }

            _logger.LogInformation("Data loaded successful from the file.");

            return records;
        }

        /// <inheritdoc />
        public void CreateReportFile(IEnumerable<ReportData> reportData, ConvertRequest request)
        {
            _logger.LogInformation("Started to create report/output file.");

            FileInfo reportFile = new FileInfo(request.FilePath);

            var config = getCsvConfiguration(request.OutputRegion, request.Separator);

            using (var writer = new StreamWriter($"{reportFile.Directory}\\{_appSettings.ReportFileName}.csv"))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.Context.RegisterClassMap<ReportDataCSVMap>();
                csv.WriteRecords(reportData);
            }

            _logger.LogInformation("Report file created successfully.");
        }

        /// <inheritdoc />
        public IEnumerable<ReportData> CreateReportData(List<FileData> fileData)
        {
            _logger.LogInformation("Started to generate the report data.");

             var reportData =  fileData.GroupBy(x => x.Region)
                  .Select(x => new ReportData()
                  {
                      Region = x.Key,
                      LastOrderDate = x.OrderByDescending(y => y.OrderDate).FirstOrDefault().OrderDate,
                      TotalUnits = x.Sum(y => y.Units),
                      TotalCost = Math.Round(x.Sum(y => y.Units * y.UnitCost), 2),
                  });

            _logger.LogInformation("The report data generated successfully");

            return reportData;
        }
        

        private void mapStringData(RawFileData rawFileData, FileData data)
        {
            data.Region = rawFileData.Region;
            data.Item = rawFileData.Item;
            data.Rep = rawFileData.Rep;
        }

        private void mapIntData(List<RawFileData> rawData, FileData fileData, int i)
        {
            if (int.TryParse(rawData[i].Units, out int number))
            {
                fileData.Units = number;
                return;
            }

            throw new FileValidationException($"In the line {i + 1}, the {nameof(RawFileData.Units)} value is not valid");
        }

        private void mapDoubleData(ConvertRequest request, List<RawFileData> rawData, FileData fileData, int i)
        {
            string stringValue = rawData[i].UnitCost;

            char inputSeparator = getSupportedLanguageByName(request.InputRegion)
                .DoubleSeparator;

            var lan = getSupportedLanguageByName(request.InputRegion);

            stringValue = inputSeparator == ','
                ? stringValue
                : stringValue.Replace(inputSeparator, ',');

            if (double.TryParse(stringValue, NumberStyles.Any, new CultureInfo(lan.CultureInfo), out double number))
            {
                fileData.UnitCost = number;
                return;
            }

            throw new FileValidationException($"In the line {i + 1}, the {nameof(RawFileData.UnitCost)} value is not valid");
        }

        private void mapDateTimeData(ConvertRequest request, List<RawFileData> rawData, FileData fileData, int i)
        {
            string stringValue = rawData[i].OrderDate;

            var supLan = getSupportedLanguageByName(request.InputRegion);

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

        private CsvConfiguration getCsvConfiguration(string regionName, char separator)
        {
            var lan = getSupportedLanguageByName(regionName);

            var config = new CsvConfiguration(new CultureInfo(lan.CultureInfo))
            {
                Delimiter = separator.ToString(),
            };

            return config;
        }

        private SupportedLanguage getSupportedLanguageByName(string name)
            => _appSettings.SupportedLanguages
               .Where(x => x.Name.Equals(name))
               .FirstOrDefault();
    }
}


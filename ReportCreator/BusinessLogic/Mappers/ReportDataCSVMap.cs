using CsvHelper.Configuration;
using ReportCreator.BusinessLogic.Data;

namespace ReportCreator.Mappers
{
    /// <summary>
    /// Mapper class from ReportData to output file content
    /// </summary>
    public sealed class ReportDataCSVMap : ClassMap<ReportData>
    {
        public ReportDataCSVMap()
        {
            Map(m => m.LastOrderDate).Name("Last OrderDate");
            Map(m => m.Region).Name("Region");
            Map(m => m.TotalUnits).Name("Total Units");
            Map(m => m.TotalCost).Name("Total Cost");
        }
    }
}

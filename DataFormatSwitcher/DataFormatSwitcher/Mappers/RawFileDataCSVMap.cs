using CsvHelper.Configuration;
using ReportCreator.Data;

namespace ReportCreator.Mappers
{
    public sealed class RawFileDataCSVMap : ClassMap<RawFileData>
    {
        public RawFileDataCSVMap()
        {
            Map(m => m.UnitCost).Name("Unit Cost");
            Map(m => m.OrderDate).Name(nameof(RawFileData.OrderDate));
            Map(m => m.Item).Name(nameof(RawFileData.Item));
            Map(m => m.Region).Name(nameof(RawFileData.Region));
            Map(m => m.Rep).Name(nameof(RawFileData.Rep));
            Map(m => m.Units).Name(nameof(RawFileData.Units));
        }
    }
}

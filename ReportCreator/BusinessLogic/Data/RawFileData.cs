namespace ReportCreator.BusinessLogic.Data
{
    /// <summary>
    /// Dto for raw file data, before any conversion
    /// </summary>
    public class RawFileData
    {
        /// <summary>
        /// Order date
        /// </summary>
        public string OrderDate { get; set; }

        /// <summary>
        /// Region
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Rep
        /// </summary>
        public string Rep { get; set; }

        /// <summary>
        /// Item
        /// </summary>
        public string Item { get; set; }

        /// <summary>
        /// Units
        /// </summary>
        public string Units { get; set; }

        /// <summary>
        /// Unit cost
        /// </summary>
        public string UnitCost { get; set; }
    }
}

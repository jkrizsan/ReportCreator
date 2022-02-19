using System;

namespace ReportCreator.BusinessLogic.Data
{
    /// <summary>
    /// Dto for parsed data
    /// </summary>
    public class FileData
    {
        /// <summary>
        /// Order date
        /// </summary>
        public DateTime OrderDate { get; set; }

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
        public int Units { get; set; }

        /// <summary>
        /// Unit cost
        /// </summary>
        public double UnitCost { get; set; }
    }
}
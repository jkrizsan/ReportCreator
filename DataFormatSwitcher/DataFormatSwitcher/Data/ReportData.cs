using System;

namespace ReportCreator.Data
{
    /// <summary>
    /// Report/output file data
    /// </summary>
    public class ReportData
    {
        /// <summary>
        /// Date of the last order
        /// </summary>
        public DateTime LastOrderDate { get; set; }

        /// <summary>
        /// Region
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Summarized units amount for a region
        /// </summary>
        public int TotalUnits { get; set; }

        /// <summary>
        /// Summarized cost amount for a region
        /// </summary>
        public double TotalCost { get; set; }
    }
}

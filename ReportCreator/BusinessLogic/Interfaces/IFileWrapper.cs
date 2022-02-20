namespace ReportCreator.BusinessLogic.Interfaces
{
    /// <summary>
    /// File Wrapper Interface
    /// </summary>
    public interface IFileWrapper
    {
        /// <summary>
        /// Check  that a file exists or not
        /// </summary>
        /// <param name="path"></param>
        /// <returns>boolean</returns>
        bool Exists(string path);
    }
}

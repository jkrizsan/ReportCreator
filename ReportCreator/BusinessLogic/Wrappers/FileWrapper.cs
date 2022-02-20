using System.IO;
using ReportCreator.BusinessLogic.Interfaces;

namespace ReportCreator.BusinessLogic.Wrappers
{
    public class FileWrapper : IFileWrapper
    {
        /// <inheritdoc />
        public bool Exists(string path)
        {
            return File.Exists(path);
        }
    }
}

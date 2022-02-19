using ReportCreator.BusinessLogic.Interfaces;
using System.IO;

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

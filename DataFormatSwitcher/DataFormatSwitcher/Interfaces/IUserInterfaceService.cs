using DataFormatSwitcher.Data;

namespace DataFormatSwitcher.Interfaces
{
    /// <summary>
    /// Interface for UserInterface Service
    /// </summary>
    public interface IUserInterfaceService
    {
        /// <summary>
        /// Build request for data format conversions based on the console input arguments
        /// </summary>
        /// <param name="args"></param>
        /// <returns>ConvertRequests</returns>
        ConvertRequest BuildRequest(string[] args);

        /// <summary>
        /// Write out to console the needed information if user call the app with the -h flag
        /// </summary>
        /// <param name="args"></param>
        void HelpUser(string[] args);
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace Netrom.WindowsService.Utils
{
    class Util
    {
        /// <summary>
        /// Get the file extension
        /// </summary>
        /// <param name="fileNameWithExtension">File name with the extension</param>
        /// <param name="withDot">A parameter that determines whether the extension returns with or without dot</param>
        /// <returns></returns>
        public static String GetFileExtension(String fileNameWithExtension, bool withDot = false) {

            if(withDot)
            {
                return Path.GetExtension(fileNameWithExtension);
            }

            return Path.GetExtension(fileNameWithExtension).Trim('.');
        }

        /// <summary>
        /// Get the destination folder path from the configuration
        /// </summary>
        /// <returns>The destination path</returns>
        public static string GetDestinationPath()
        {
            string destinationFolder = String.Empty;

            try
            {
                destinationFolder = ConfigurationManager.AppSettings["destinationFolder"];
            }

            catch (Exception)
            {
                Console.WriteLine("Destination path is missing.");
            }

            return destinationFolder;
        }

        /// <summary>
        /// Get the shared folder path from the configuration
        /// </summary>
        /// <returns>The shared path</returns>
        public static string GetSharedPath()
        {
            string sharedFolder = String.Empty;

            try
            {
                sharedFolder = ConfigurationManager.AppSettings["sharedFolder"];
            }

            catch (Exception)
            {
                Console.WriteLine("Shared path is missing.");
            }

            return sharedFolder;
        }
    }

}

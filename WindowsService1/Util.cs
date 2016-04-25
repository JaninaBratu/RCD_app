using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace WindowsService1
{
    class Util
    {
        public static String getFileExtension(String file) {

            return Path.GetExtension(file).Trim('.');
        }

        //Get destination path from configuration
        internal static string GetDestinationPath()
        {
            string value = String.Empty;

            try
            {
                value = ConfigurationManager.AppSettings["destinationFolderPath"];
            }

            catch (Exception)
            {
                Console.WriteLine("Destination path is missing.");
            }


            return value;

        }

        //Get source path from configuration
        public static string GetSharedPath()
        {
            string scanFolderPath = String.Empty;

            try
            {
                scanFolderPath = ConfigurationManager.AppSettings["shareFolderPath"];
            }

            catch (Exception)
            {
                Console.WriteLine("Source path is missing.");
            }

            return scanFolderPath;

        }
    }
}

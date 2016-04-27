using Netrom.WindowsService.Utils;
using Netrom.WindowsService.SharingFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Netrom.WindowsService.SharingFiles
{
    /// <summary>
    /// This class is responsible for the sharing process
    /// </summary>
    class Sharing
    {
        #region private variables
        private string _sharedFolder; //share folder location
        private string _destinationFolder; //destination folder location
        #endregion


        /// <summary>
        /// Constructor; initialize the private variables
        /// </summary>
        public Sharing()
        {
            _sharedFolder      = Util.GetSharedPath();
            _destinationFolder = Util.GetDestinationPath();
        }

        public void StartSharing()
        {

            try
            {
                //Verify if the shared folder exists
                if (Directory.Exists(_sharedFolder))
                {
                    //Get the list of files found in shared folder
                    string[] sharedFiles = Directory.GetFiles(_sharedFolder);

                    //If folder is not empty
                    if (sharedFiles != null)
                    {
                        string fileName;
                        //Executing a loop through the files
                        foreach (string filePath in sharedFiles)
                        {
                            fileName = Path.GetFileName(filePath);

                            FileSharing file = new FileSharing(fileName, _sharedFolder, _destinationFolder);
                            //Starts the file's sharing process
                            file.StartFileSharingProcess();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

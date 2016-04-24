using System;
using System.IO;

namespace WindowsService1
{
    class ScanFile
    {
        private string shareFolderPath = "E:\\training";

        Util util = new Util();

        public string sharedFolderPath { get; set; }


        #region processFile
        public void processFile()
        {
            
            string fileExtension = "";
            string file = "";

            try
            {
                //verify to see if the path of the shareFolder exists
                if (Directory.Exists(shareFolderPath))
                {
                    //in order to process the files from the shareFolder, we first need to see if there are any files to process
                    Boolean isEmpty = util.IsDirectoryEmpty(shareFolderPath);
                    if (!isEmpty)
                    {
                        foreach (string f in Directory.GetFiles(shareFolderPath))
                        {
                            file = Path.GetFileName(f);
                            fileExtension = util.getFileExtension(file);
                            processFolders(shareFolderPath, file, fileExtension);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion


        #region processFolders
        private void processFolders(string shareFolderPath, string file, string fileExtension)
        {
            Boolean isDirectoryEmpty = false;
            Boolean equalName = false;
            Boolean equalContent = false;

            string fullSourcePath = Path.Combine(shareFolderPath, file);

            string destinationPath = Path.Combine(shareFolderPath, fileExtension);

            string fullDestinationPath = Path.Combine(destinationPath, file);

            try {

                //craete the directory if it not exist
                if (!Directory.Exists(destinationPath))
                {
                    Directory.CreateDirectory(destinationPath);
                }

                else
                {
                    isDirectoryEmpty = util.IsDirectoryEmpty(destinationPath);
                    equalName = checkFileName(file, destinationPath);
                    equalContent = checkContentFile(fullSourcePath, destinationPath);

                    //if the name of the 2 files are equal and the content is not the same
                    // first: rename the file
                    //second: move the file
                     if (!isDirectoryEmpty && equalName && !equalContent)
                     {
                            string updatedFileName = util.renameFileName(fullSourcePath);
                            fullDestinationPath = Path.Combine(destinationPath, updatedFileName);
                     }
                     
                     //if the name of the 2 files are the same and the content is also the same
                     //delete the file
                    if (!isDirectoryEmpty && equalName && equalContent)
                    {
                        util.deleteFile(fullSourcePath);
                    }

                }

                //only if we have files in the source folder, we can move them to the specified one
                if (!isDirectoryEmpty)
                {
                    File.Move(fullSourcePath, fullDestinationPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
        #endregion


        #region checkFileName
        public Boolean checkFileName(string fileName, string destinationPath)
        {
            foreach(string f in Directory.GetFiles(destinationPath))
                if (Path.GetFileName(fileName).Equals(Path.GetFileName(f)))
                {
                    return true;
                }
            return false;
        }
        #endregion


        #region checkContentFile
        public Boolean checkContentFile(string fullSourcePath, string destinationPath)
        {
            //comment? 
            foreach (string f in Directory.GetFiles(destinationPath))
            {
                using (var reader1 = new FileStream(fullSourcePath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    using (var reader2 = new FileStream(f, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        byte[] hash1;
                        byte[] hash2;

                        using (var md51 = new System.Security.Cryptography.MD5CryptoServiceProvider())
                        {
                            md51.ComputeHash(reader1);
                            hash1 = md51.Hash;
                        }

                        using (var md52 = new System.Security.Cryptography.MD5CryptoServiceProvider())
                        {
                            md52.ComputeHash(reader2);
                            hash2 = md52.Hash;
                        }

                        int j = 0;
                        for (j = 0; j < hash1.Length; j++)
                        {
                            if (hash1[j] != hash2[j])
                            {
                                break;
                            }
                        }

                        //if both hashed contents are identical
                        if (j == hash1.Length)
                        {
                            return true;
                        }

                        return false;
                    }
                }

            }
            return false;
        }
        #endregion

    }
}

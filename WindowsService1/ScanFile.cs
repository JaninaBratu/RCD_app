using System;
using System.IO;

namespace WindowsService1
{
    class ScanFile
    {

        private string shareFolderPath = "D:\\source";
        private string destinationFolderPath = "D:\\destination";

        #region processFile
        public void processFile()
        {
            try
            {
                //verify to see if the path of the shareFolder exists
                if (Directory.Exists(shareFolderPath))
                {
                    // Get the list of files found in the directory.
                    string[] scanFolderFiles = Directory.GetFiles(shareFolderPath);
                    if (scanFolderFiles != null)
                    {
                        string fileName;
                        foreach (string f in scanFolderFiles)
                        {
                            fileName = Path.GetFileName(f);
                       
                            processFolder(fileName);
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


        #region processFolder
        private void processFolder(string fileName)
        {
            string fileExtension = Util.getFileExtension(fileName);
            Boolean equalContent = false;

            string fullSourcePath = Path.Combine(shareFolderPath, fileName);

            string fullDestinationPath = Path.Combine(destinationFolderPath, fileExtension);
            string updatedFileName = fileName;
            try {


                //craete the directory if it not exist
                if (!Directory.Exists(destinationFolderPath))
                {
                    // Try to create the directory.
                    try
                    {
                        Directory.CreateDirectory(fullDestinationPath);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("The directory couldn't been created : {0}", e.ToString());
                    }
                } else
                {
                    equalContent = checkContentFile(fullSourcePath, fullDestinationPath);
                    updatedFileName = setFileName(fileName, fileExtension, fullDestinationPath);
                    
                }

                //if file is already in destination, delete it
                if (equalContent == true)
                {
                    deleteFile(fullSourcePath);
                } else 
                {
                    fullDestinationPath = Path.Combine(fullDestinationPath, updatedFileName);
                    moveFile(fullSourcePath, fullDestinationPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
        #endregion

        //Verify if is another file in the transition folder with the same name
        private string setFileName(string fileName, string extension, string newFullPath)
        {
            int count = 0;

            string tempFileName = Path.GetFileNameWithoutExtension(fileName);
            string extensionWithDot = "." + extension;

            string baseNewFullPath = newFullPath;
            newFullPath = newFullPath + Path.DirectorySeparatorChar + tempFileName;
         
            //while there is a file with the same name
            while (File.Exists(newFullPath + extensionWithDot))
            {
                //change the file name scanned
                tempFileName = string.Format("{0}({1})", tempFileName, count++);
                newFullPath = Path.Combine(baseNewFullPath, tempFileName);
            }

            return tempFileName + extensionWithDot;
        }



        #region checkContentFile
        public Boolean checkContentFile(string fullSourcePath, string destinationPath)
        {
         
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

        #region deleteFile()
        //Delete the file
        private void deleteFile(string filePath)
        {
            try
            {
                System.IO.File.Delete(filePath);
            }
            catch (Exception)
            {
                throw new FileNotFoundException("The file couldn't been deleted!");
            }
        }
        #endregion


        #region moveFile()
        //Move the file
        private void moveFile(string filePath, string movingPath)
        {
            try
            {
                //To move a file or folder to a new location:
                File.Move(filePath, movingPath);
            }
            catch (Exception)
            {
                throw new FileNotFoundException("The file couldn't been moved!");
            }
        }
        #endregion
    }
}

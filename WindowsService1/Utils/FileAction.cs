using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netrom.WindowsService.Utils
{
    class FileAction
    {
        #region FilesCompare
        /// <summary>
        /// Verify if both hashed contents are identical
        /// </summary>
        /// <param name="filePath1">Location of the first file</param>
        /// <param name="filePath2">Location of the second file</param>
        /// <returns></returns>
        public static Boolean FilesCompare(string filePath1, string filePath2)
        {
            using (var reader1 = new FileStream(filePath1, FileMode.Open, FileAccess.Read))
            {
                using (var reader2 = new FileStream(filePath2, FileMode.Open, FileAccess.Read))
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

                    //Verify if both hashed contents are identical
                    if (j == hash1.Length)
                    {
                        return true;
                    }

                    return false;
                }
            }
        }
        #endregion

        #region DeleteFile()
        /// <summary>
        /// Delete the file
        /// </summary>
        /// <param name="filePath">File location</param>
        public static void DeleteFile(string filePath)
        {
            try
            {
                System.IO.File.Delete(filePath);
            }
            catch (Exception)
            {
                Console.WriteLine("The file couldn't been deleted!");
            }
        }
        #endregion


        #region MoveFile()
        /// <summary>
        /// Move file
        /// </summary>
        /// <param name="filePath">File location</param>
        /// <param name="destinationPath">Destination location</param>
        public static void MoveFile(string filePath, string destinationPath)
        {
            try
            {
                //To move a file or folder to a new location:
                File.Move(filePath, destinationPath);
            }
            catch (Exception)
            {
                Console.WriteLine("The file couldn't been moved!");
            }
        }
        #endregion
    }
}

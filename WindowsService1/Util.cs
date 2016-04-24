using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService1
{
    class Util
    {
        public String getFileExtension(String file) {

            return Path.GetExtension(file).Trim('.');
        }

        public bool IsDirectoryEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }

        public string renameFileName(string fileName)
        {
            int count = 0;
            string newFileName = "";

            while (File.Exists(fileName))
            {
                newFileName = string.Concat(Path.GetFileNameWithoutExtension(fileName), count++);
            }
            return Path.Combine(newFileName, Path.GetExtension(fileName));
        }

        public void deleteFile(string filePath)
        {
                try
                {
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                    
                }
                catch (System.IO.IOException e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }
               
        }

        
    }
}

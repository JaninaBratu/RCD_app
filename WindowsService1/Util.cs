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

        public string renameFileName(string filePath)
        {
            int count = 0;
            string newFileName = "";

            if (File.Exists(filePath))
            {
                newFileName = string.Concat(Path.GetFileNameWithoutExtension(filePath), count++);
            }
            return string.Concat(newFileName, Path.GetExtension(filePath));
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

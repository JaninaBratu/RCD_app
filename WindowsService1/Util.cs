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


    }
}

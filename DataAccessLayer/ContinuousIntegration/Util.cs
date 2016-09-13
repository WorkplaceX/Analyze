using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ContinuousIntegration
{
    public class Util
    {
        public static string FileLoad(string fileName)
        {
            return File.ReadAllText(fileName);
        }

        public static void FileSave(string fileName, string text)
        {
            File.WriteAllText(fileName, text);
        }
    }
}

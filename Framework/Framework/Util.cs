namespace Framework
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    public static class Util
    {
        public static string VersionServer
        {
            get
            {
                return "v0.2 Server";
            }
        }

        /// <summary>
        /// Gets root FolderName.
        /// </summary>
        public static string FolderName
        {
            get
            {
                Uri uri = new Uri(typeof(Util).GetTypeInfo().Assembly.CodeBase);
                string result;
                if (uri.AbsolutePath.Contains("/bin/Debug/")) // Running in Visual Studio
                {
                    result = new Uri(uri, "../../../../").AbsolutePath;
                }
                else
                {
                    result = new Uri(uri, "./").AbsolutePath;
                }
                return result;
            }
        }

        public static string FileRead(string fileName)
        {
            return File.ReadAllText(fileName);
        }

        public static void FileWrite(string fileName, string value)
        {
            lock (typeof(object))
            {
                File.WriteAllText(fileName, value);
            }
        }

        public static string[] FileNameList(string folderName)
        {
            var result = Directory.GetFiles(folderName, "*.*", SearchOption.AllDirectories).OrderBy(item => item).ToArray();
            return result;
        }
    }
}

namespace Application
{
    using Newtonsoft.Json;
    using System;
    using System.Reflection;

    public static class ConnectionManager
    {
        public static ConfigServer ConfigServer
        {
            get
            {
                return ConfigServer.Instance;
            }
        }

        public static string ConnectionString
        {
            get
            {
                return ConfigServer.ConnectionStringDev;
            }
        }

        public static void FolderName(out string folderName, out bool isRunningIIS)
        {
            Uri uri = new Uri(typeof(ConnectionManager).GetTypeInfo().Assembly.CodeBase);
            string result;
            if (uri.AbsolutePath.Contains("/bin/Debug/")) // Running in Visual Studio
            {
                result = new Uri(uri, "../../../../").AbsolutePath;
                isRunningIIS = false;
            }
            else
            {
                result = new Uri(uri, "./").AbsolutePath;
                isRunningIIS = true;
            }
            folderName = result;
        }
    }

    /// <summary>
    /// Server config json.
    /// </summary>
    public class ConfigServer
    {
        public string ConnectionStringDev;

        public string ConnectionStringProd;

        public static string JsonFileName
        {
            get
            {
                string folderName;
                bool isRunningIIS;
                ConnectionManager.FolderName(out folderName, out isRunningIIS);
                string fileName = folderName;
                if (!isRunningIIS)
                {
                    fileName += "Server/";
                }
                fileName += "ConnectionManager.json"; // See also .gitignore
                return fileName;
            }
        }

        public static string JsonTxtFileName
        {
            get
            {
                string folderName;
                bool isRunningIIS;
                ConnectionManager.FolderName(out folderName, out isRunningIIS);
                string fileName = folderName;
                if (!isRunningIIS)
                {
                    fileName += "Server/";
                }
                fileName += "ConnectionManager.json.txt"; // See also .gitignore
                return fileName;
            }
        }

        public static ConfigServer Instance
        {
            get
            {
                string json = Util.FileRead(JsonFileName); // See also .gitignore
                var result = JsonConvert.DeserializeObject<ConfigServer>(json);
                return result;
            }
        }
    }
}

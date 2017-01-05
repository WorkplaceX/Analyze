namespace Application
{
    using Newtonsoft.Json;
    using System;
    using System.Reflection;

    public class ConnectionStringConfig
    {
        public string Local { get; set; }

        public string Remote { get; set; }
    }

    public static class ConnectionManager
    {
        public static ConnectionStringConfig ConnectionStringConfig
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
                fileName += "ConnectionString.json"; // See also .gitignore
                string json = Util.FileRead(fileName);
                ConnectionStringConfig result = JsonConvert.DeserializeObject<ConnectionStringConfig>(json);
                return result;
            }
        }

        public static string ConnectionString
        {
            get
            {
                return ConnectionStringConfig.Remote;
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
}

namespace Build
{
    using System;
    using System.IO;
    using System.Reflection;
    using Newtonsoft.Json;

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
                string json = Util.FileRead(ConnectionManager.FolderName + "Server/ConnectionString.json"); // See also .gitignore
                ConnectionStringConfig result = JsonConvert.DeserializeObject<ConnectionStringConfig>(json);
                return result;
            }
        }

        public static string ConnectionString
        {
            get
            {
                return ConnectionStringConfig.Local;
            }
        }

        /// <summary>
        /// Gets "Framework" folder.
        /// </summary>
        public static string FolderName
        {
            get
            {
                Uri uri = new Uri(typeof(ConnectionManager).GetTypeInfo().Assembly.CodeBase);
                return new Uri(uri, "../../../../").AbsolutePath;
            }
        }

        public static string NpmFileName
        {
            get
            {
                return "C:/Program Files/nodejs/npm.cmd";
            }
        }

        public static string DotNetFileName
        {
            get
            {
                return "dotnet.exe";
            }
        }

        public static string CodeFileName
        {
            get
            {
                string result = "C:/Program Files/Microsoft VS Code/code.exe";
                if (!File.Exists(result))
                {
                    result = "C:/Program Files (x86)/Microsoft VS Code/code.exe";
                }
                return result;
            }
        }

        public static string NodeFileName
        {
            get
            {
                return "C:/Program Files/nodejs/node.exe";
            }
        }
    }
}

namespace Build
{
    using System;
    using System.IO;
    using System.Reflection;
    using Newtonsoft.Json;
    using System.Data.SqlClient;

    public static class ConnectionManager
    {
        public static ConfigServer ConfigServer
        {
            get
            {
                return ConfigServer.Instance;
            }
        }

        public static ConfigBuild ConfigBuild
        {
            get
            {
                return Build.ConfigBuild.Instance;
            }
        }

        public static string ConnectionString
        {
            get
            {
                return ConfigServer.ConnectionStringDev;
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
                string result = "npm.cmd";
                foreach (string fileName in ConfigBuild.NpmFileName)
                {
                    if (File.Exists(fileName))
                    {
                        result = fileName;
                    }
                }
                return result;
            }
        }

        public static string DotNetFileName
        {
            get
            {
                return "dotnet.exe";
            }
        }

        public static string VisualStudioCodeFileName
        {
            get
            {
                string result = "code.exe";
                foreach (string fileName in ConfigBuild.VisualStudioCodeFileName)
                {
                    if (File.Exists(fileName))
                    {
                        result = fileName;
                    }
                }
                return result;
            }
        }

        public static string MSBuildFileName
        {
            get
            {
                string result = "MSBuild.exe";
                foreach (string fileName in ConfigBuild.MSBuildFileName)
                {
                    if (File.Exists(fileName))
                    {
                        result = fileName;
                    }
                }
                return result;
            }
        }

        public static string NodeFileName
        {
            get
            {
                string result = "node.exe";
                foreach (string fileName in ConfigBuild.NodeFileName)
                {
                    if (File.Exists(fileName))
                    {
                        result = fileName;
                    }
                }
                return result;
            }
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
                return ConnectionManager.FolderName + "Server/ConnectionManager.json"; // See also .gitignore
            }
        }

        public static string JsonTxtFileName
        {
            get
            {
                return ConnectionManager.FolderName + "Server/ConnectionManager.json.txt"; // ConnectionManager template.
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

    /// <summary>
    /// Build config json.
    /// </summary>
    public class ConfigBuild
    {
        public string[] NodeFileName;

        public string[] NpmFileName;

        public string[] VisualStudioCodeFileName;

        public string[] MSBuildFileName;

        public static string JsonFileName
        {
            get
            {
                return ConnectionManager.FolderName + "Build/ConnectionManager.json";
            }
        }

        public static ConfigBuild Instance
        {
            get
            {
                string json = Util.FileRead(JsonFileName);
                var result = JsonConvert.DeserializeObject<ConfigBuild>(json);
                return result;
            }
        }
    }

    public static class ConnectionManagerCheck
    {
        /// <summary>
        /// Check dev connection string.
        /// </summary>
        private static void ConnectionStringCheck()
        {
            if (!File.Exists(ConfigServer.JsonFileName))
            {
                File.Copy(ConfigServer.JsonTxtFileName, ConfigServer.JsonFileName);
            }
            string connectionString = ConnectionManager.ConfigServer.ConnectionStringDev;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    Util.Log("SQL Connection check...");
                    connection.Open();
                    Util.Log("SQL Connection [ok]");
                }
                catch
                {
                    Util.Log(string.Format("Error: Connection to Dev SQL Server failed! ({0})", ConfigServer.JsonFileName));
                }
            }
        }

        private static void FileNameCheck()
        {
            if (!File.Exists(ConnectionManager.NodeFileName))
            {
                Util.Log(string.Format("Error: File not found! ({0}; {1})", ConnectionManager.NodeFileName, ConfigBuild.JsonFileName));
            }
            if (!File.Exists(ConnectionManager.NpmFileName))
            {
                Util.Log(string.Format("Error: File not found! ({0}; {1})", ConnectionManager.NpmFileName, ConfigBuild.JsonFileName));
            }
            if (!File.Exists(ConnectionManager.VisualStudioCodeFileName))
            {
                Util.Log(string.Format("Warning: File not found! Visual Studio Code. ({0}; {1})", ConnectionManager.VisualStudioCodeFileName, ConfigBuild.JsonFileName));
            }
            if (!File.Exists(ConnectionManager.MSBuildFileName))
            {
                Util.Log(string.Format("Error: File not found! ({0}; {1})", ConnectionManager.MSBuildFileName, ConfigBuild.JsonFileName));
            }
        }

        public static void Run()
        {
            ConnectionStringCheck();
            FileNameCheck();
        }
    }
}

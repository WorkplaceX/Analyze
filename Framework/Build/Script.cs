namespace Build
{
    using System;
    using System.Data.SqlClient;

    public class DescriptionAttribute : Attribute
    {
        public DescriptionAttribute(string text)
        {
            this.Text = text;
        }

        public readonly string Text;
    }

    public static class Script
    {
        [Description("npm install; dotnet restore; Script error Python can be ignored")]
        public static void InstallAll()
        {
            Util.Log("Client>npm install");
            Util.NpmInstall(ConnectionManager.FolderName + "Client/");
            Util.Log("Server>npm install");
            Util.NpmInstall(ConnectionManager.FolderName + "Server/");
            Util.Log("Universal>npm install");
            Util.NpmInstall(ConnectionManager.FolderName + "Universal/", false); // Throws always an exception!
            // Application
            Util.Log("Application>dotnet restore");
            Util.DotNetRestore(ConnectionManager.FolderName + "Application/");
            Util.Log("Application>dotnet build");
            Util.DotNetBuild(ConnectionManager.FolderName + "Application/");
            // Server
            Util.Log("Server>dotnet restore");
            Util.DotNetRestore(ConnectionManager.FolderName + "Server/");
            Util.Log("Server>dotnet build");
            Util.DotNetBuild(ConnectionManager.FolderName + "Server/");
            RunGulp();
        }

        [Description("VS Code")]
        public static void OpenClient()
        {
            Util.OpenCode(ConnectionManager.FolderName + "Client/");
        }

        [Description("npm run start")]
        public static void StartClient()
        {
            Util.NpmRun(ConnectionManager.FolderName + "Client/", "start");
        }

        [Description("npm run gulp; Run everytime when Client changes")]
        public static void RunGulp()
        {
            Util.Log("Server>npm run gulp");
            Util.NpmRun(ConnectionManager.FolderName + "Server/", "gulp");
        }

        [Description("VS Code")]
        public static void OpenServer()
        {
            Util.OpenCode(ConnectionManager.FolderName + "Server/");
        }

        [Description("VS Code")]
        public static void OpenUniversal()
        {
            Util.OpenCode(ConnectionManager.FolderName + "Universal/");
        }

        [Description("Start Server and UniversalExpress")]
        public static void StartServerAndClient()
        {
            Util.DotNetRun(ConnectionManager.FolderName + "Server/", false);
            Util.Node(ConnectionManager.FolderName + "UniversalExpress/Universal/", "index.js", false);
            Util.OpenBrowser("http://localhost:5000");
        }

        [Description("Open Framework.sln")]
        public static void OpenVisualStudio()
        {
            Util.OpenBrowser(ConnectionManager.FolderName + "Framework.sln");
        }

        private static void PublishSql(string connectionString)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            var fileNameList = Util.FileNameList(ConnectionManager.FolderName + "Build/Sql/");
            foreach (string fileName in fileNameList)
            {
                string text = Util.FileRead(fileName);
                var sqlList = text.Split(new string[] { "\r\nGO" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string sql in sqlList)
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        [Description("Publish to local SQL server and generate CSharp DTO's")]
        public static void DataAccessLayerLocal()
        {
            Airport.Script.Run();
            PublishSql(ConnectionManager.ConnectionStringConfig.Local);
            Build.DataAccessLayer.Script.Run();
        }

        [Description("Publish to remote SQL server and generate CSharp DTO's")]
        public static void DataAccessLayerRemote()
        {
            Airport.Script.Run();
            PublishSql(ConnectionManager.ConnectionStringConfig.Remote);
            Build.DataAccessLayer.Script.Run();
        }

        [Description("Run unit tests")]
        public static void UnitTest()
        {
            Util.DotNetRun(ConnectionManager.FolderName + "UnitTest/");
        }
    }
}
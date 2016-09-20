namespace ContinuousIntegration
{
    using System;
    using System.Reflection;

    public static class ConnectionManager
    {
        public static string ConnectionString
        {
            get
            {
                return @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=Debug;Integrated Security=True";
            }
        }

        public static string FolderName
        {
            get
            {
                return new Uri(new Uri(Assembly.GetEntryAssembly().Location), ".").LocalPath;
            }
        }

        public static string SchemaFileName
        {
            get
            {
                return FolderName + @"Sql\schema.sql";
            }
        }

        public static string DatabaseLockFileName
        {
            get
            {
                return new Uri(new Uri(FolderName), @"..\..\..\..\WebApplication\Database.lock.cs").LocalPath;
            }
        }

        public static void Log(string text)
        {
            Console.WriteLine(text);
        }
    }
}

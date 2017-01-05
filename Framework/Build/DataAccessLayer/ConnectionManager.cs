namespace Build.DataAccessLayer
{
    public static class ConnectionManager
    {
        public static string SchemaFileName
        {
            get
            {
                return Build.ConnectionManager.FolderName + "Build/DataAccessLayer/Sql/Schema.sql";
            }
        }

        public static string DatabaseLockFileName
        {
            get
            {
                return Build.ConnectionManager.FolderName + "Application/DataAccessLayer/Database.lock.cs";
            }
        }

        public static string ConnectionString
        {
            get
            {
                return Build.ConnectionManager.ConnectionString;
            }
        }
    }
}

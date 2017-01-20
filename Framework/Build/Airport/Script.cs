namespace Build.Airport
{
    public class Script
    {
        /// <summary>
        /// Load Airport.xlsx into database.
        /// </summary>
        public static void Run()
        {
            string connectionString = ConnectionManager.ConnectionString;
            string fileName = ConnectionManager.FolderName + "Office/bin/Debug/Office.exe";
            // SqlDrop
            {
                string command = "SqlDrop";
                string arguments = command + " " + "\"" + connectionString + "\"";
                Util.Start(ConnectionManager.FolderName, fileName, arguments);
            }
            // SqlCreate
            {
                string command = "SqlCreate";
                string arguments = command + " " + "\"" + connectionString + "\"";
                Util.Start(ConnectionManager.FolderName, fileName, arguments);
            }
            // Run
            {
                string command = "Run";
                string folderName = ConnectionManager.FolderName + "Build/Airport/";
                string arguments = command + " " + "\"" + connectionString + "\"" + " " + "\"" + folderName + "\"";
                Util.Start(ConnectionManager.FolderName, fileName, arguments);
            }
        }
    }
}

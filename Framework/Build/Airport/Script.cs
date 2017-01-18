namespace Build.Airport
{
    public class Script
    {
        /// <summary>
        /// Load Airport.xlsx into database.
        /// </summary>
        public static void Run()
        {
            // SqlCreate
            string fileName = ConnectionManager.FolderName + "Office/bin/Debug/Office.exe";
            string command = "SqlCreate";
            string connectionString = ConnectionManager.ConnectionString;
            string arguments = command + " " + "\"" + connectionString + "\"";
            Util.Start(null, fileName, arguments);
            // Run
            command = "Run";
            string folderName = ConnectionManager.FolderName + "Build/Airport/";
            arguments = command + " " + "\"" + connectionString + "\"" + " " + "\"" + folderName + "\"";
            Util.Start(null, fileName, arguments);
        }
    }
}

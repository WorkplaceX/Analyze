namespace WorkplaceX.Cli
{
    using System.Reflection;

    internal static class UtilCli
    {
        /// <summary>
        /// Gets Version. This is the version defined in file (*.csproj) VersionPrefix.
        /// </summary>
        public static string Version
        {
            get
            {
                var result = ((AssemblyInformationalVersionAttribute)typeof(UtilCli).Assembly.GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute)).First()).InformationalVersion;
                return result;
            }
        }

        /// <summary>
        /// Write to stderr.
        /// </summary>
        internal static void ConsoleWriteLineError(object value)
        {
            using TextWriter textWriter = Console.Error;
            textWriter.WriteLine(value);
            Environment.ExitCode = 1; // echo %errorlevel% # Run in command prompt
        }

        /// <summary>
        /// Write to console in color.
        /// </summary>
        internal static void ConsoleWriteLineColor(object value, ConsoleColor? color, bool isLine = true)
        {
            if (color == null)
            {
                if (isLine)
                {
                    Console.WriteLine(value);
                }
                else
                {
                    Console.Write(value);
                }
            }
            else
            {
                Console.ForegroundColor = color.Value;
                if (isLine)
                {
                    Console.WriteLine(value);
                }
                else
                {
                    Console.Write(value);
                }
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Gets FolderNameSln. This is the root folder where App.sln is located. Returns null for example if installed as global package.
        /// </summary>
        internal static string? FolderNameSln
        {
            get
            {
                string? result = null;
                Uri uri = new Uri(typeof(UtilCli).Assembly.Location);
                uri = new Uri(uri, "../../../../../");
                if (File.Exists(uri.AbsolutePath + "App.sln"))
                {
                    result = uri.AbsolutePath;
                }
                return result;
            }
        }

        /// <summary>
        /// Returns all files. Also in sub folder.
        /// </summary>
        internal static List<string> FileNameList(string folderName, string searchPattern)
        {
            return Directory.GetFiles(folderName, searchPattern, SearchOption.AllDirectories).Select(item => item.Replace("\\", "/")).ToList();
        }

        /// <summary>
        /// Returns all files. Also in sub folder.
        /// </summary>
        internal static List<string> FileNameList(string folderName)
        {
            return FileNameList(folderName, "*.*");
        }

        /// <summary>
        /// Copy file and create folder.
        /// </summary>
        internal static void FileNameCopy(string fileNameSource, string fileNameDest)
        {
            string folderNameDest = new FileInfo(fileNameDest).DirectoryName!;
            if (!Directory.Exists(folderNameDest))
            {
                Directory.CreateDirectory(folderNameDest);
            }
            File.Copy(fileNameSource, fileNameDest);
        }
    }
}

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
        /// Gets FolderNameAssembly. This is the folder of the executing assembly.
        /// </summary>
        internal static string FolderNameAssembly
        {
            get
            {
                return typeof(UtilCli).Assembly.Location;
            }
        }

        /// <summary>
        /// Returns FolderNameCurrent. This is the current working directory.
        /// </summary>
        internal static string FolderNameCurrent
        {
            get
            {
                var result = Directory.GetCurrentDirectory().Replace("\\", "/");
                if (!result.EndsWith("/"))
                {
                    result += "/";
                }
                return result;
            }
        }

        /// <summary>
        /// Gets FolderNameFrameworkSln. This is the folder where file Framework.sln is located. Returns null if for example installed as global package.
        /// </summary>
        internal static string? FolderNameFrameworkSln
        {
            get
            {
                string? result = null;
                Uri uri = new Uri(typeof(UtilCli).Assembly.Location);
                uri = new Uri(uri, "../../../../");
                if (File.Exists(uri.AbsolutePath + "Framework.sln"))
                {
                    result = uri.AbsolutePath;
                }
                return result;
            }
        }

        /// <summary>
        /// Gets FolderNameContent. This is the folder where file Framework.Template.zip is located. Returns null if for example not installed as global package.
        /// </summary>
        internal static string? FolderNameContent
        {
            get
            {
                string? result = null;
                Uri uri = new Uri(typeof(UtilCli).Assembly.Location);
                uri = new Uri(uri, "../../../content/");
                if (File.Exists(uri.AbsolutePath + "Framework.Template.zip"))
                {
                    result = uri.AbsolutePath;
                }
                return result;
            }
        }

        /// <summary>
        /// Gets FolderNameAppCliExe. This is the folder where file App.Cli.exe is located. Returns null if for example not built or current working directory is not a WorkplaceX application.
        /// </summary>
        internal static string? FolderNameAppCliExe
        {
            get
            {
                string? result = null;
                Uri uri = new Uri(FolderNameCurrent);
                uri = new Uri(uri, "App.Cli/bin/Debug/net6.0/"); // net6.0
                if (File.Exists(uri.AbsolutePath + "App.Cli.exe"))
                {
                    result = uri.AbsolutePath;
                }
                return result;
            }
        }

        /// <summary>
        /// Gets FolderNameAppCliCsproj. This is the folder where file App.Cli.csproj is located. Returns null if for example current working directory is not a WorkplaceX application.
        /// </summary>
        internal static string? FolderNameAppCliCsproj
        {
            get
            {
                string? result = null;
                Uri uri = new Uri(FolderNameCurrent);
                uri = new Uri(uri, "App.Cli/");
                if (File.Exists(uri.AbsolutePath + "App.Cli.csproj"))
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

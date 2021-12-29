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
        /// Gets FolderNameSln. This is the root folder where App.sln is located.
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
    }
}

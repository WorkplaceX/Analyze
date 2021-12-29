namespace WorkplaceX.Cli.App
{
    using Microsoft.Extensions.CommandLineUtils;
    using System.Diagnostics;
    using System.IO.Compression;

    internal class AppCli
    {
        private CommandLineApplication commandLineApplication { get; } = new CommandLineApplication();

        public void Run(string[] args)
        {
            // Title
            commandLineApplication.FullName = "WorkplaceX.Cli";
            commandLineApplication.HelpOption("-h | --help"); // Command line interface help (to show commands)
            commandLineApplication.VersionOption("-v | --version", UtilCli.Version);
            
            // Register command new project
            commandLineApplication.Command("new", (configuration) =>
            {
                configuration.Description = "Create new project";
                configuration.OnExecute(() => Command(configuration, CommandNewProject));
            });

            // Register command templateZip
            if (UtilCli.FolderNameFrameworkSln != null)
            {
                var folderNameTemplate = new Uri(new Uri(UtilCli.FolderNameFrameworkSln), "Framework.Template/").AbsolutePath;
                if (Directory.Exists(folderNameTemplate))
                {
                    commandLineApplication.Command("templateZip", (configuration) =>
                    {
                        configuration.Description = "Zip folder Framework.Template/ before pack.";
                        configuration.OnExecute(() => Command(configuration, CommandTemplateZip));
                    });
                }
            }
            
            // Show list of available commands
            if (args.Length > 0)
            {
                commandLineApplication.Execute("-h"); // Show list of available commands.
            }

            // Debug
            Console.WriteLine("FolderNameAssembly=" + UtilCli.FolderNameAssembly);
            Console.WriteLine("FolderNameCurrent=" + UtilCli.FolderNameCurrent);
            Console.WriteLine("FolderNameFrameworkSln=" + UtilCli.FolderNameFrameworkSln);
            Console.WriteLine("FolderNameContent=" + UtilCli.FolderNameContent);
            Console.WriteLine("FolderNameAppCliExe=" + UtilCli.FolderNameAppCliExe);
            Console.WriteLine("FolderNameAppCliCsproj=" + UtilCli.FolderNameAppCliCsproj);

            // Execute command
            try
            {
                commandLineApplication.Execute(args);
            }
            catch (Exception exception) // For example unrecognized option
            {
                UtilCli.ConsoleWriteLineError(exception);
            }
        }

        public static int Command(CommandLineApplication command, Action action)
        {
            UtilCli.ConsoleWriteLineColor($"Command run ({ command.Name })", ConsoleColor.Green);
            action();
            UtilCli.ConsoleWriteLineColor($"Command success! ({ command.Name })", ConsoleColor.Green);
            return 0;
        }

        /// <summary>
        /// Create new project from Framework.Template/ into empty folder.
        /// </summary>
        public static void CommandNewProject()
        {
            Console.WriteLine("Create new project...");
        }

        /// <summary>
        /// Zip folder Framework.Template/ before running dotnet pack.
        /// </summary>
        public static void CommandTemplateZip()
        {
            Console.WriteLine("Create Framework.Template.zip");

            var folderNameTemplate = new Uri(new Uri(UtilCli.FolderNameFrameworkSln!), "Framework.Template/").AbsolutePath;
            var fileNameList = UtilCli.FileNameList(folderNameTemplate);
            
            // Filter folder node_modules, bin, obj, vs
            fileNameList = fileNameList.Where(item => !item.Contains("/node_modules/") && !item.Contains("/bin/") && !item.Contains("/obj/") && !item.Contains("/.vs/")).ToList();
            
            // Temp FolderName
            var folderNameTemp = Path.GetTempPath().Replace("\\", "/") + Guid.NewGuid() + "/" + "Framework.Template/";
            Directory.CreateDirectory(folderNameTemp);

            // Copy FileName
            foreach (var fileName in fileNameList)
            {
                Debug.Assert(fileName.StartsWith(folderNameTemplate));
                var fileNameDest = folderNameTemp + fileName.Substring(folderNameTemplate.Length);
                UtilCli.FileNameCopy(fileName, fileNameDest);
            }

            // Zip
            var fileNameZip = UtilCli.FolderNameFrameworkSln + "WorkplaceX.Cli/Framework.Template.zip";
            if (File.Exists(fileNameZip))
            {
                File.Delete(fileNameZip);
            }
            ZipFile.CreateFromDirectory(folderNameTemp, fileNameZip);
            Directory.Delete(folderNameTemp, recursive: true);
        }
    }
}

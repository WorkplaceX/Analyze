namespace WorkplaceX.Cli.App
{
    using Microsoft.Extensions.CommandLineUtils;

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
                configuration.OnExecute(() => CommandNewProject());
            });

            // Register command templateZip
            if (UtilCli.FolderNameSln != null)
            {
                var folderNameTemplate = new Uri(new Uri(UtilCli.FolderNameSln), "Framework/Framework.Template/").AbsolutePath;
                if (Directory.Exists(folderNameTemplate))
                {
                    commandLineApplication.Command("templateZip", (configuration) =>
                    {
                        configuration.Description = "Zip folder Framework.Template/ before pack.";
                        configuration.OnExecute(() => CommandTemplateZip());
                    });
                }
            }
            
            // Show list of available commands
            if (args.Length > 0)
            {
                commandLineApplication.Execute("-h"); // Show list of available commands.
            }
            
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

        /// <summary>
        /// Create new project from Framework.Template/ into empty folder.
        /// </summary>
        public static int CommandNewProject()
        {
            Console.WriteLine("Create new project...");
            Console.WriteLine(typeof(UtilCli).Assembly.Location);
            return 0;
        }

        /// <summary>
        /// Zip folder Framework.Template/ before running dotnet pack.
        /// </summary>
        public static int CommandTemplateZip()
        {
            var d = UtilCli.FolderNameSln;
            return 0;
        }
    }
}

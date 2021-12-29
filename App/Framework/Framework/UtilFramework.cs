namespace Framework
{
    using System.Reflection;

    public static class UtilFramework
    {
        /// <summary>
        /// Gets Version. This is the version defined in file (*.csproj) VersionPrefix.
        /// </summary>
        public static string Version
        {
            get
            {
                // dotnet--version # 6.0.101
                // node --version # v16.13.1
                // npm--version # 8.1.2
                // ng --version # Angular CLI: 13.1.2
                // git--version # git version 2.34.1.windows.1

                var result = ((AssemblyInformationalVersionAttribute)typeof(UtilFramework).Assembly.GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute)).First()).InformationalVersion;
                return result;
            }
        }
    }
}

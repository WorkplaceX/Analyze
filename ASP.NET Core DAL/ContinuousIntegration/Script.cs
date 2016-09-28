namespace ContinuousIntegration
{
    using System.Text;

    /// <summary>
    /// Generate CSharp code for database tables.
    /// </summary>
    public class Script
    {
        /// <summary>
        /// Script to generate CSharp code.
        /// </summary>
        public void Run()
        {
            MetaSql metaSql = new MetaSql();
            MetaCSharp metaCSharp = new MetaCSharp(metaSql);
            StringBuilder result = new StringBuilder();
            string cSharp;
            new CSharpGenerate(metaCSharp).Run(out cSharp);
            Util.FileSave(ConnectionManager.DatabaseLockFileName, cSharp);
        }
    }
}

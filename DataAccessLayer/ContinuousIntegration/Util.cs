namespace ContinuousIntegration
{
    using System.IO;

    public class Util
    {
        public static string FileLoad(string fileName)
        {
            return File.ReadAllText(fileName);
        }

        public static void FileSave(string fileName, string text)
        {
            File.WriteAllText(fileName, text);
        }
    }
}

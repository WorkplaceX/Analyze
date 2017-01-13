namespace Build
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Util.Log("");
            Util.Log("Build Command");
            Util.MethodExecute(typeof(Script));
            // Console.Write(">");
            // Console.ReadLine();
        }
    }
}

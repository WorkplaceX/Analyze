using Framework.Build;

namespace Build
{
    public class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                Util.Log("");
                Util.Log("Build Command");
                Util.MethodExecute(new Script());
            }
        }
    }
}

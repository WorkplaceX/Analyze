namespace ContinuousIntegration
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            ConnectionManager.Log("Run method Script.Run(); ...");
            new Script().Run();
            ConnectionManager.Log($"File generated successfully! ({ ConnectionManager.DatabaseLockFileName})");
            Console.ReadLine();
        }
    }
}

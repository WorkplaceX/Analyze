namespace ContinuousIntegration
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Run method GenerateCSharp.Run(); ...");
            GenerateCSharp.Run();
            Console.WriteLine($"File generated successfully! ({ ConnectionManager.DatabaseLockFileName})");
            Console.ReadLine();
        }
    }
}

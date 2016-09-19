namespace Test
{
    using System;
    using System.Reflection;

    public class Program
    {
        public static void Main(string[] args)
        {
            new FrameworkTest().Run();
            Console.WriteLine();
            Console.WriteLine("All test successful!");
            Console.ReadLine();
        }
    }

    /// <summary>
    /// Base class for tests.
    /// </summary>
    public abstract class TestBase
    {
        /// <summary>
        /// Run invokes all parameterless methods.
        /// </summary>
        public void Run()
        {
            Type type = GetType();
            foreach (var method in type.GetTypeInfo().GetMethods())
            {
                if (method.GetParameters().Length == 0)
                {
                    if (method.DeclaringType == type) // Filter out for example method ToString();
                    {
                        method.Invoke(this, new object[] { }); // Invoke method on static class.
                        Console.WriteLine($"Method {type.Name}.{method.Name}(); successful!");
                    }
                }
            }
        }
    }
}

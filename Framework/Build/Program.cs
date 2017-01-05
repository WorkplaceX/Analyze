namespace Build
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public class Program
    {
        public static void Main(string[] args)
        {
            Util.Log("Build command:");
            Util.MethodExecute(typeof(Script));
            // Console.Write(">");
            // Console.ReadLine();
        }
    }
}

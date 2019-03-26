using System;
using Knight.Nuget.Lib;

namespace Knight.AddPackageConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var str = "Hello World!";
            str = str.AppendKnight();
            Console.WriteLine(str);
            Console.ReadLine();
        }
    }
}

using System;
using System.Globalization;
using ClassLibrary1;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Console.WriteLine($"Console: {CultureInfo.CurrentUICulture.Name}");
            CultureInfo.CurrentUICulture = CultureInfo.CreateSpecificCulture("ja");
            Console.WriteLine($"Console: {CultureInfo.CurrentUICulture.Name}");
            Console.WriteLine($"Library: {Class1.GetLocale()}");

            Console.ReadLine();
        }
    }
}

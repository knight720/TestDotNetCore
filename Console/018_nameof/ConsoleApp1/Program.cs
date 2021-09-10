using System;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine(Season.Summer.ToString());
            Console.WriteLine(nameof(Season.Summer));
        }
    }

    internal enum Season
    {
        Spring,
        Summer,
        Fall,
        Winter
    }
}
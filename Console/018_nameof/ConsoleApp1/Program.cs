using System;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine(Season.Summer.ToString());
            Console.WriteLine(nameof(Season.Summer));
            var season = Season.Summer;
            Console.WriteLine(nameof(season));
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
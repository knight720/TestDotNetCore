using Microsoft.Extensions.Configuration;
using System;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables("KNIGHT_")
                .AddCommandLine(args)
                .Build();

            var firstKey = config["FIRSTKEY"];

            Console.WriteLine($"Hello World! {DateTime.Now.ToShortTimeString()}");
            Console.WriteLine($"First: {firstKey}");
        }
    }
}
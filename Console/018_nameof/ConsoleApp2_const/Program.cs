using System;

namespace ConsoleApp2_const
{
    internal class Program
    {
        private int var_normal = 1;
        private readonly int var_readonly = 2;
        private const int var_const = 3;

        private static void Main(string[] args)
        {
            new Program().Print();
        }

        private void Print()
        {
            Console.WriteLine(var_normal);
            Console.WriteLine(var_readonly);
            Console.WriteLine(var_const);
        }
    }
}
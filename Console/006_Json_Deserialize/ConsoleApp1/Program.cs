using System;
using ConsoleApp1.ZipCode;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var zipCode = new ZipCodeService();
            var data = zipCode.GetZipCode("MY");

            Console.ReadLine();
        }
    }
}

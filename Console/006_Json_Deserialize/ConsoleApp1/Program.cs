using System;
using ConsoleApp1.ZipCode;
using SomeLibrary;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var zipCode = new ZipCodeService();
            var data = zipCode.GetZipCode("MY");

            var someCode = new SomeService();
            var someData = someCode.GetZipCode("Data");

            Console.ReadLine();
        }
    }
}

using System;
using System.Globalization;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = CultureInfo.GetCultures(CultureTypes.AllCultures);
            Console.WriteLine(JsonConvert.SerializeObject(result));
            Console.ReadLine();
        }
    }
}

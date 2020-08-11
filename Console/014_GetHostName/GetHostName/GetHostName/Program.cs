using System;
using System.Net;

namespace GetHostName
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Dns.GetHostName());
        }
    }
}

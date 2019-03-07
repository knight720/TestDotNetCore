using System;
using Shipment;

namespace ShipmentConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var sm = new ShipmentManager();
            sm.GetCourier();
            Console.WriteLine("Hello World");
        }
    }
}

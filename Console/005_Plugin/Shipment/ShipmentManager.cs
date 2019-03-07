using System;
using System.Reflection;
using System.Linq;

namespace Shipment
{
    public class ShipmentManager
    {
        IShipment _shipment;

        public ShipmentManager()
        {
            var assembly = Assembly.LoadFile(@"D:\Code\Test\TestDotNetCore\Console\005_Plugin\FirstShipment\bin\Debug\netstandard2.0\FirstShipment.dll");
            var modules = assembly.GetLoadedModules();
            modules.ToList().ForEach(i => Console.WriteLine(i.Name));
            //this._shipment = (IShipment)assembly.CreateInstance("FirstShipment.FirstShipment");
        }

        public object GetCourier()
        {
            return this._shipment.GetCourier();
        }
    }
}

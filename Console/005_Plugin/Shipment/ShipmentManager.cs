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
            var assembly = Assembly.LoadFile(@"D:\Code\TestDotNetCore\Console\005_Plugin\FirstShipment\bin\Debug\netstandard2.0\FirstShipment.dll");
            this._shipment = (IShipment)assembly.CreateInstance("FirstShipment.FirstShipment");
        }

        public object GetCourier()
        {
            return this._shipment.GetCourier();
        }
    }

}

using System;
using Shipment;
using System.Collections.Generic;

namespace FirstShipment
{
    public class FirstShipment : IShipment
    {
        public IEnumerable<object> GetCourier()
        {
            Console.WriteLine("FirstShipment");
            return null;
        }
    }
}

using System;
using System.Collections.Generic;

namespace Shipment
{
    public interface IShipment
    {
        IEnumerable<object> GetCourier();
    }
}

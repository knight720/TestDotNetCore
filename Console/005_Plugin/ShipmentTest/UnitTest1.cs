using System;
using Xunit;
using Shipment;

namespace ShipmentTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var manager = new ShipmentManager();
            manager.GetCourier();
            
            Assert.True(true);
        }
    }
}

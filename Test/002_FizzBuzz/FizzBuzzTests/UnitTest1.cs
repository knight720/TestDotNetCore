using System;
using Xunit;

namespace FizzBuzzTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            FizzBuzzService target = new FizzBuzzService();
            string actual = target.GetData(1);

            Assert.Equal("1", actual);

        }
    }
}

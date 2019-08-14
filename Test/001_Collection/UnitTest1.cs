using System;
using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace _001_Collection
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            IEnumerable collection = new[] { 1, 2, 5, 8 };

            collection.Should().NotBeEmpty()
                .And.HaveCount(4)
                .And.ContainInOrder(new[] { 2, 5 })
                .And.ContainItemsAssignableTo<int>();

            collection.Should().Equal(new List<int> { 1, 2, 5, 8 });
            collection.Should().Equal(1, 2, 5, 8);
            collection.Should().BeEquivalentTo(8, 2, 1, 5);
            collection.Should().NotBeEquivalentTo(new[] { 8, 2, 3, 5 });
        }
    }
}

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

        [Fact]
        public void Test2()
        {
            IEnumerable collection = new[]
            {
                new Entity()
                {
                    Value = "A"
                }
            };

            collection.Should().BeEquivalentTo(new List<Entity> { new Entity() { Value = "A" } });
        }

        [Fact]
        public void Test3()
        {
            IEnumerable collection = new[]
            {
                new Entity()
                {
                    Value = "A"
                }
            };

            collection.Should().NotBeEquivalentTo(new List<Entity> { new Entity() { Value = "B" } });
        }

        [Fact]
        public void Test4()
        {
            IList<IEntity> collection = new[]
            {
                new EntityB()
                {
                    ValueB = "A"
                }
            };
            collection.Should().BeEquivalentTo(new List<IEntity> { new EntityB() { ValueB = "A" } });
        }

        [Fact]
        public void Test5()
        {
            IList<IEntity> collection = new[]
            {
                new EntityB()
                {
                    ValueB = "A"
                }
            };

            collection.Should().NotBeEquivalentTo(new List<IEntity> { new EntityB() { ValueB = "B" } });
        }
    }

    public class Entity
    {
        public string Value { get; set; }
    }

    public interface IEntity
    {
        string ValueI { get; set; }
    }

    public class EntityB : IEntity
    {
        public string ValueB { get; set; }
        public string ValueI { get; set; }
    }
}
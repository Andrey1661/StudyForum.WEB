using System;
using System.Collections.Generic;
using System.Linq;
using DynamicQueryFilter.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicQueryFilter.Tests
{
    [TestClass]
    public class DynamicFilterTest
    {
        public class Filter
        {
            [FilterProperty(CompareTypes.Equal, "Name")]
            public string FilterName { get; set; }

            [FilterProperty(CompareTypes.MoreThanOrEqual, "IntValue")]
            public int IntFilter { get; set; }
        }

        public class Entity
        {
            public string Name { get; set; }
            public int IntValue { get; set; }
        }

        [TestMethod]
        public void TestMethod1()
        {
            var df = new DynamicFilter<Filter, Entity>();
            var filter = new Filter {FilterName = "Andrey", IntFilter = 5};
            var entities = new List<Entity>
            {
                new Entity {Name = "Andrey", IntValue = 5},
                new Entity {Name = "Nikolay", IntValue = 5},
                new Entity {Name = "Danil", IntValue = 1}
            };

            var query = df.CreateWhereExpression(filter);
            var func = query.Compile();
            var result = entities.Where(func).ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Andrey", result.First().Name);
        }
    }
}

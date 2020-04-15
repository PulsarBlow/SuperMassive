namespace SuperMassive.Tests.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;
    using SuperMassive.Extensions;

    public class EnumerableExtensionsTest
    {
        [Test]
        public void EnumerableExtensions_Each_WithNull_ItemArgument_Throws()
        {
            IEnumerable<string> collection = null;
            Assert.Throws<ArgumentNullException>(() => collection.Each(x => { return; }));
        }

        [Test]
        public void EnumerableExtensions_Each_With_NullDelegate_Throws()
        {
            IEnumerable<string> collection = new List<string>().AsEnumerable();
            Assert.Throws<ArgumentNullException>(() => collection.Each(null));
        }

        [Test]
        public void EnumerableExtensions_Each_Execute_Delegate_On_All_CollectionElements()
        {
            int counter = 0;
            IEnumerable<string> collection = new List<string> {"a", "b", "c"};
            collection.Each(x => counter++);
            Assert.IsTrue(counter == collection.Count());
        }
    }
}

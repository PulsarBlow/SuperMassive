namespace SuperMassive.Tests.Extensions
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;
    using SuperMassive.Extensions;

    public class ListExtensionsTest
    {
        [Test]
        public void ListExtensions_AddIfNotNull_With_NullExtended_Throws()
        {
            List<string> list = null;
            Assert.Throws<ArgumentNullException>(() => list.AddIfNotNull("item"));
        }

        [Test]
        public void ListExtensions_AddIfNotNull_With_Null_Item_DoesNotAdd()
        {
            List<string> list = new List<string>();
            list.AddIfNotNull(null);

            Assert.That(list, Does.Not.Contain(null));
        }

        [Test]
        public void ListExtensions_AddIfNotNull_With_ValidItem_Adds()
        {
            List<string> list = new List<string>();
            list.AddIfNotNull("a");

            Assert.That(list, Does.Contain("a"));
        }

        [Test]
        public void ListExtensions_AddRangeIfNotNull_With_NullExtended_Throws()
        {
            List<string> list = null;
            Assert.Throws<ArgumentNullException>(() => list.AddRangeIfNotNull(new[] { "a" }));
        }

        [Test]
        public void ListExtensions_AddRangeIfNotNull_With_NullItemCollection_DoesNotAdd_DoesNotThrows()
        {
            List<string> list = new List<string>();
            list.AddRangeIfNotNull(null);
        }

        [Test]
        public void ListExtensions_AddRangeIfNotNull_With_ValidItemCollection_Adds()
        {
            List<string> list = new List<string>();
            list.AddRangeIfNotNull(new[] { "a" });

            Assert.That(list, Does.Contain("a"));
        }
    }
}

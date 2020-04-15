namespace SuperMassive.Tests.Extensions
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;
    using SuperMassive.Extensions;

    public class ListExtensionsTest
    {
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

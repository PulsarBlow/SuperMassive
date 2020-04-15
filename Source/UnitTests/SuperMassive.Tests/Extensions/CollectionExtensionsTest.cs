namespace SuperMassive.Tests.Extensions
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;
    using SuperMassive.Extensions;

    public class CollectionExtensionsTest
    {
        [Test]
        public void ListExtensions_AddIfNotNull_With_NullExtended_Throws()
        {
            ICollection<string> list = null;
            Assert.Throws<ArgumentNullException>(() => list.AddIfNotNull("item"));
        }

        [Test]
        public void ListExtensions_AddIfNotNull_With_Null_Item_DoesNotAdd()
        {
            var list = new List<string>();
            list.AddIfNotNull(null);

            Assert.That(list, Does.Not.Contain(null));
        }

        [Test]
        public void ListExtensions_AddIfNotNull_With_ValidItem_Adds()
        {
            var list = new List<string>();
            list.AddIfNotNull("a");

            Assert.That(list, Does.Contain("a"));
        }
    }
}

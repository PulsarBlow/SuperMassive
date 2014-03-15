using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperMassive.Identity.TableStorage;
using System;

namespace SuperMassive.Identity.TableStorageTests
{
    [TestClass]
    public class UserEntityPartitionKeyResolverTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_WithOutOfRangeNumberOfBuckets()
        {
            UserPartitionKeyResolver resolver = new UserPartitionKeyResolver(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Resolve_WithNullEntityId()
        {
            UserPartitionKeyResolver resolver = new UserPartitionKeyResolver();
            resolver.Resolve(null);
        }

        [TestMethod]
        public void Resolve_WithSuccess()
        {
            UserPartitionKeyResolver resolver = new UserPartitionKeyResolver("SomePrefix");
            string expected = "SomePrefix_93";
            string actual = resolver.Resolve(Guid.Empty.ToString());
            Assert.AreEqual(expected, actual);

            resolver = new UserPartitionKeyResolver(1, "SomePrefix");
            expected = "SomePrefix_1";
            actual = resolver.Resolve(Guid.Empty.ToString());
            Assert.AreEqual(expected, actual);
        }
    }
}

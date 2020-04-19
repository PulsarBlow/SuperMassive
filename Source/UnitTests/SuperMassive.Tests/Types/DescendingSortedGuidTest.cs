namespace SuperMassive.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using NUnit.Framework;

    public class DescendingSortedGuidTest
    {
        [Test]
        public void DescendingSortedGuid_CreateNewSortedGuid_Returns_New_DescendingSortedGuid()
        {
            var desc = DescendingSortedGuid.NewSortedGuid();
            Assert.IsNotNull(desc);
            Assert.IsFalse(string.IsNullOrWhiteSpace(desc.ToString()));
        }

        [Test]
        public void DescendingSortedGuid_TryParse_With_Valid_DescendingSortedGuid_Returns_True()
        {
            DescendingSortedGuid guid;
            Assert.IsTrue(DescendingSortedGuid.TryParse("0635318522499400050_B77AD6F9624A4C2896E8545923E56502",
                out guid));
            Assert.AreEqual("0635318522499400050_b77ad6f9624a4c2896e8545923e56502", guid.ToString());
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("abcd")]
        public void DescendingSortedGuid_TryParse_With_NotValid_DescendingSortedGuid_Returns_False(string value)
        {
            DescendingSortedGuid guid;
            Assert.IsFalse(DescendingSortedGuid.TryParse(value, out guid));
        }

        [Test]
        public void DescendingSortedGuid_Parse_With_ValidFormat_Returns_ParsedItem()
        {
            DescendingSortedGuid expected = DescendingSortedGuid.NewSortedGuid();
            DescendingSortedGuid actual = DescendingSortedGuid.Parse(expected.ToString());

            AssertCompare(expected, actual);
        }

        [Test]
        public void DescendingSortedGuid_Parse_With_NullArgument_Throws_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => DescendingSortedGuid.Parse(null));
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("abcde")]
        [TestCase("{BE700357-B0F3-475D-A0EA-C7DFB8AF8BFA}")]
        [TestCase("BE700357-B0F3-475D-A0EA-C7DFB8AF8BFA")]
        public void DescendingSortedGuid_Parse_With_InvalidFormat_Throws_ArgumentException(string value)
        {
            Assert.Throws<ArgumentException>(() => DescendingSortedGuid.Parse(value));
        }

        [Test]
        public void DescendingSortedGuid_Order_WithSuccess()
        {
            var expected = new List<Fake<DescendingSortedGuid>>();

            for (var i = 0; i < 3; i++)
            {
                var item = NewItemDesc();
                expected.Add(item);
                Thread.Sleep(10); // slowing down
            }

            var actual = expected.OrderByDescending(x => x.Id.ToString()).ToList();
            Assert.That(actual, Is.EqualTo(expected));

            actual = expected.OrderByDescending(x => x.Id).ToList();
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void DescendingSortedGuid_LexicalOrdering_Descending_WithSuccess()
        {
            var timestamp = DateTimeOffset.UtcNow;
            var expected = new List<DescendingSortedGuid>
            {
                new DescendingSortedGuid(timestamp, Guid.NewGuid()),
                new DescendingSortedGuid(timestamp.AddMilliseconds(-10), Guid.NewGuid()),
                new DescendingSortedGuid(timestamp.AddMilliseconds(-100), Guid.NewGuid()),
                new DescendingSortedGuid(timestamp.AddMilliseconds(-1000), Guid.NewGuid()),
            };

            var actual = expected.OrderBy(x => x.ToString()).ToList();
            Assert.That(actual, Is.EqualTo((expected)));

            actual = expected.OrderBy(x => x).ToList();
            Assert.That(actual, Is.EqualTo((expected)));
        }

        [Test]
        public void DescendingSortedGuid_Operator_Equals_Returns_True()
        {
            var guid = Guid.NewGuid();
            var timestamp = DateTimeOffset.UtcNow;

            var value1 = new DescendingSortedGuid(timestamp, guid);
            var value2 = new DescendingSortedGuid(timestamp, guid);

            Assert.IsTrue(value1 == value2);
        }

        [Test]
        public void DescendingSortedGuid_Operator_Equals_Returns_False()
        {
            var guid = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            var timestamp = DateTimeOffset.UtcNow;
            var timestamp2 = DateTimeOffset.MaxValue;

            var value1 = new DescendingSortedGuid(timestamp, guid);
            var value2 = new DescendingSortedGuid(timestamp, guid);

            Assert.IsFalse(new DescendingSortedGuid(timestamp, guid) == new DescendingSortedGuid(timestamp, guid2));
            Assert.IsFalse(new DescendingSortedGuid(timestamp2, guid) == new DescendingSortedGuid(timestamp, guid));
        }

        [Test]
        public void DescendingSortedGuid_GetHasCode_WithFastIteration_Creates_UniqueHasCode()
        {
            var hashSet = new HashSet<int>();
            for (var i = 0; i < 1000; i++)
            {
                var hashCode = DescendingSortedGuid.NewSortedGuid().GetHashCode();
                Assert.IsTrue(hashSet.Add(hashCode), $"Duplicate hashcode: {hashCode}");
            }
        }

        [Test]
        public void DescendingSortedGuid_Equals_With_EqualItems_Returns_True()
        {
            var guid = Guid.NewGuid();
            var timestamp = DateTimeOffset.UtcNow;

            Assert.IsTrue(
                new DescendingSortedGuid(timestamp, guid).Equals((object) new DescendingSortedGuid(timestamp, guid)));
            Assert.IsTrue(new DescendingSortedGuid(timestamp, guid).Equals(new DescendingSortedGuid(timestamp, guid)));
            Assert.IsTrue(new DescendingSortedGuid(timestamp, guid).Equals(new DescendingSortedGuid(timestamp, guid)));
            Assert.IsTrue(DescendingSortedGuid.Empty.Equals((object) DescendingSortedGuid.Empty));
            Assert.IsTrue(DescendingSortedGuid.Empty.Equals(DescendingSortedGuid.Empty));
        }

        [Test]
        public void DescendingSortedGuid_Equals_With_NotEqualItems_Returns_False()
        {
            Assert.IsFalse(new DescendingSortedGuid().Equals(null));
            Assert.IsFalse(new DescendingSortedGuid().Equals(new {Toto = "toto"}));
            Assert.IsFalse(DescendingSortedGuid.NewSortedGuid().Equals(DescendingSortedGuid.NewSortedGuid()));
        }

        [Test]
        public void DescendingSortedGuid_Operator_LessOrEquals_Returns_True()
        {
            var guid = Guid.NewGuid();
            var timestamp = DateTimeOffset.UtcNow;
            var smallerTimestamp = timestamp.AddMilliseconds(-1);

            Assert.IsTrue(new DescendingSortedGuid(timestamp, guid) <= new DescendingSortedGuid(timestamp, guid));
            Assert.IsTrue(new DescendingSortedGuid(smallerTimestamp, guid) <=
                          new DescendingSortedGuid(timestamp, guid));
        }

        [Test]
        public void DescendingSortedGuid_OperatorOverload_LessOrEquals_Returns_False()
        {
            var guid = Guid.NewGuid();
            var timestamp = DateTimeOffset.UtcNow;
            var biggerTimestamp = timestamp.AddMilliseconds(1);

            Assert.IsFalse(new DescendingSortedGuid(biggerTimestamp, guid) <=
                           new DescendingSortedGuid(timestamp, guid));
        }

        [Test]
        public void DescendingSortedGuid_OperatorOverload_MoreOrEquals_Returns_True()
        {
            var guid = Guid.NewGuid();
            var timestamp = DateTimeOffset.UtcNow;
            var smallerTimestamp = timestamp.AddMilliseconds(-1);

            Assert.IsTrue(new DescendingSortedGuid(timestamp, guid) >= new DescendingSortedGuid(timestamp, guid));
            Assert.IsTrue(new DescendingSortedGuid(timestamp, guid) >=
                          new DescendingSortedGuid(smallerTimestamp, guid));
        }

        [Test]
        public void DescendingSortedGuid_OperatorOverload_MoreOrEquals_Returns_False()
        {
            var guid = Guid.NewGuid();
            var timestamp = DateTimeOffset.UtcNow;
            var biggerTimestamp = timestamp.AddMilliseconds(1);

            Assert.IsFalse(new DescendingSortedGuid(timestamp, guid) >=
                           new DescendingSortedGuid(biggerTimestamp, guid));
        }

        [Test]
        public void DescendingSortedGuid_OperatorOverload_StriclyLessThan_Returns_True()
        {
            var guid = Guid.NewGuid();
            var timestamp = DateTimeOffset.UtcNow;
            var smallerTimestamp = timestamp.AddMilliseconds(-1);

            Assert.IsTrue(new DescendingSortedGuid(smallerTimestamp, guid) < new DescendingSortedGuid(timestamp, guid));
        }

        [Test]
        public void DescendingSortedGuid_OperatorOverload_StriclyLessThan_Returns_False()
        {
            var guid = Guid.NewGuid();
            var timestamp = DateTimeOffset.UtcNow;

            Assert.IsFalse(new DescendingSortedGuid(timestamp, guid) < new DescendingSortedGuid(timestamp, guid));
        }

        [Test]
        public void DescendingSortedGuid_OperatorOverload_StriclyMoreThan_Returns_True()
        {
            var guid = Guid.NewGuid();
            var timestamp = DateTimeOffset.UtcNow;
            var smallerTimestamp = timestamp.AddMilliseconds(-1);

            Assert.IsTrue(new DescendingSortedGuid(timestamp, guid) > new DescendingSortedGuid(smallerTimestamp, guid));
        }

        [Test]
        public void DescendingSortedGuid_OperatorOverload_StriclyMoreThan_Returns_False()
        {
            var guid = Guid.NewGuid();
            var timestamp = DateTimeOffset.UtcNow;

            Assert.IsFalse(new DescendingSortedGuid(timestamp, guid) > new DescendingSortedGuid(timestamp, guid));
        }

        [Test]
        public void DescendingSortedGuid_OperatorOverload_DifferentThan_Returns_True()
        {
            var guid = Guid.NewGuid();
            var timestamp = DateTimeOffset.UtcNow;
            var smallerTimestamp = timestamp.AddMilliseconds(-1);

            Assert.IsTrue(new DescendingSortedGuid(timestamp, guid) !=
                          new DescendingSortedGuid(smallerTimestamp, guid));
            Assert.IsTrue(new DescendingSortedGuid(smallerTimestamp, guid) !=
                          new DescendingSortedGuid(timestamp, guid));
            Assert.IsTrue(DescendingSortedGuid.NewSortedGuid() != DescendingSortedGuid.NewSortedGuid());
        }

        [Test]
        public void DescendingSortedGuid_OperatorOverload_DifferentThan_Returns_False()
        {
            var guid = Guid.NewGuid();
            var timestamp = DateTimeOffset.UtcNow;
            var empty1 = DescendingSortedGuid.Empty;
            var empty2 = DescendingSortedGuid.Empty;

            Assert.IsFalse(new DescendingSortedGuid(timestamp, guid) != new DescendingSortedGuid(timestamp, guid));
            Assert.IsFalse(new DescendingSortedGuid() != new DescendingSortedGuid());
            Assert.IsFalse(empty1 != empty2);
        }

        [Test]
        public void DescendingSortedGUid_CompareToObject_With_NullArgument_Returns_PlusOne()
        {
            object obj = null;
            Assert.IsTrue(new DescendingSortedGuid().CompareTo(obj) == 1);
        }

        [Test]
        public void DescendingSortedGUid_CompareToObject_With_NotAssignableType_Returns_PlusOne()
        {
            object obj = new {value = "value"};
            Assert.IsTrue(new DescendingSortedGuid().CompareTo(obj) == 1);
        }

        [Test]
        public void DescendingSortedGuid_CompareToObject_Returns_Correct_Comparison_Value()
        {
            var guid = Guid.NewGuid();
            var timestamp = DateTimeOffset.UtcNow;
            var greaterTimestamp = timestamp.AddMilliseconds(1);

            Assert.IsTrue(
                new DescendingSortedGuid(timestamp, guid).CompareTo(
                    (object) new DescendingSortedGuid(greaterTimestamp, guid)) == 1);
            Assert.IsTrue(
                new DescendingSortedGuid(greaterTimestamp, guid).CompareTo(
                    (object) new DescendingSortedGuid(timestamp, guid)) == -1);
            Assert.IsTrue(
                new DescendingSortedGuid(timestamp, guid).CompareTo(
                    (object) new DescendingSortedGuid(timestamp, guid)) == 0);
        }

        private static Fake<DescendingSortedGuid> NewItemDesc()
        {
            var item = new Fake<DescendingSortedGuid>();
            item.Id = DescendingSortedGuid.NewSortedGuid();
            item.Name = Randomizer.GetRandomString();
            return item;
        }

        private static void AssertCompare(DescendingSortedGuid expected, DescendingSortedGuid actual)
        {
            Assert.AreEqual(expected.Guid, actual.Guid);
            Assert.AreEqual(expected.Timestamp, actual.Timestamp);
        }

        private static void AssertCompare(Fake<DescendingSortedGuid> expected, Fake<DescendingSortedGuid> actual)
        {
            AssertCompare(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
        }

        private class Fake<T>
        {
            public T Id { get; set; }
            public string Name { get; set; }
        }
    }
}

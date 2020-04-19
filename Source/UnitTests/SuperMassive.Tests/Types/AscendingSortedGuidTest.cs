namespace SuperMassive.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;
    using UnitTestHelpers;

    public class AscendingSortedGuidTest
    {
        [Test]
        public void AscendingSortedGuid_CreateNewSortedGuid_Returns_New_AscendingSortedGuid()
        {
            AscendingSortedGuid idAsc = AscendingSortedGuid.NewSortedGuid();
            Assert.IsNotNull(idAsc);
            Assert.IsFalse(string.IsNullOrWhiteSpace(idAsc.ToString()));
        }

        [Test]
        public void AscendingSortedGuid_TryParse_WithValid_AscendingSortedGuid_Returns_True()
        {
            AscendingSortedGuid guid;
            Assert.IsTrue(AscendingSortedGuid.TryParse("0635318522499400050_B77AD6F9624A4C2896E8545923E56502", out guid));
            Assert.AreEqual("0635318522499400050_b77ad6f9624a4c2896e8545923e56502", guid.ToString());
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("abcd")]
        public void AscendingSortedGuid_TryParse_WithNotValid_AscendingSortedGuid_Returns_False(string value)
        {
            AscendingSortedGuid guid;
            Assert.IsFalse(AscendingSortedGuid.TryParse(value, out guid));
        }

        [Test]
        public void AscendingSortedGuid_Parse_With_ValidFormat_Returns_ParsedItem()
        {
            AscendingSortedGuid expected = AscendingSortedGuid.NewSortedGuid();
            AscendingSortedGuid actual = AscendingSortedGuid.Parse(expected.ToString());

            AssertCompare(expected, actual);
        }

        [Test]
        public void AscendingSortedGuid_Parse_With_NullArgument_Throws_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => AscendingSortedGuid.Parse(null));
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("abcde")]
        [TestCase("{BE700357-B0F3-475D-A0EA-C7DFB8AF8BFA}")]
        [TestCase("BE700357-B0F3-475D-A0EA-C7DFB8AF8BFA")]
        public void AscendingSortedGuid_Parse_With_InvalidFormat_Throws_ArgumentException(string value)
        {
            Assert.Throws<ArgumentException>(() => AscendingSortedGuid.Parse(value));
        }

        [Test]
        public void AscendingSortedGuid_LexicalOrdering_Ascending_WithSuccess()
        {
            var timestamp = DateTimeOffset.UtcNow;
            List<AscendingSortedGuid> expected = new List<AscendingSortedGuid>
            {
                new AscendingSortedGuid(timestamp, Guid.NewGuid()),
                new AscendingSortedGuid(timestamp.AddMilliseconds(10), Guid.NewGuid()),
                new AscendingSortedGuid(timestamp.AddMilliseconds(100), Guid.NewGuid()),
                new AscendingSortedGuid(timestamp.AddMilliseconds(1000), Guid.NewGuid()),
            };

            List<AscendingSortedGuid> actual = expected.OrderBy(x => x.ToString()).ToList();
            CommonComparers.AreCollectionEquals(expected, actual, AssertCompare);

            actual = expected.OrderBy(x => x).ToList();
            CommonComparers.AreCollectionEquals(expected, actual, AssertCompare);
        }

        [Test]
        public void AscendingSortedGuid_Operator_Equals_Returns_True()
        {
            var guid = Guid.NewGuid();
            var timestamp = DateTimeOffset.UtcNow;

            var value1 = new AscendingSortedGuid(timestamp, guid);
            var value2 = new AscendingSortedGuid(timestamp, guid);

            Assert.IsTrue(value1 == value2);
        }

        [Test]
        public void AscendingSortedGuid_Operator_Equals_Returns_False()
        {
            var guid = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            var timestamp = DateTimeOffset.UtcNow;
            var timestamp2 = DateTimeOffset.MaxValue;

            var value1 = new AscendingSortedGuid(timestamp, guid);
            var value2 = new AscendingSortedGuid(timestamp, guid);

            Assert.IsFalse(new AscendingSortedGuid(timestamp, guid) == new AscendingSortedGuid(timestamp, guid2));
            Assert.IsFalse(new AscendingSortedGuid(timestamp2, guid) == new AscendingSortedGuid(timestamp, guid));
        }

        [Test]
        public void AscendingSortedGuid_GetHasCode_WithFastIteration_Creates_UniqueHasCode()
        {
            HashSet<int> hashSet = new HashSet<int>();
            for (int i = 0; i < 1000; i++)
            {
                int hashCode = AscendingSortedGuid.NewSortedGuid().GetHashCode();
                Assert.IsTrue(hashSet.Add(hashCode), $"Duplicate hashcode: {hashCode}");
            }
        }

        [Test]
        public void AscendingSortedGuid_Equals_With_EqualItems_Returns_True()
        {
            var guid = Guid.NewGuid();
            var timestamp = DateTimeOffset.UtcNow;

            Assert.IsTrue(new AscendingSortedGuid(timestamp, guid).Equals((object)new AscendingSortedGuid(timestamp, guid)));
            Assert.IsTrue(new AscendingSortedGuid(timestamp, guid).Equals(new AscendingSortedGuid(timestamp, guid)));
            Assert.IsTrue(new AscendingSortedGuid(timestamp, guid).Equals(new AscendingSortedGuid(timestamp, guid)));
            Assert.IsTrue(AscendingSortedGuid.Empty.Equals((object)AscendingSortedGuid.Empty));
            Assert.IsTrue(AscendingSortedGuid.Empty.Equals(AscendingSortedGuid.Empty));
        }

        [Test]
        public void AscendingSortedGuid_Equals_With_NotEqualItems_Returns_False()
        {
            Assert.IsFalse(new AscendingSortedGuid().Equals(null));
            Assert.IsFalse(new AscendingSortedGuid().Equals(new { Toto = "toto" }));
            Assert.IsFalse(AscendingSortedGuid.NewSortedGuid().Equals(AscendingSortedGuid.NewSortedGuid()));
        }

        [Test]
        public void AscendingSortedGuid_Operator_LessOrEquals_Returns_True()
        {
            var guid = Guid.NewGuid();
            var timestamp = DateTimeOffset.UtcNow;
            var smallerTimestamp = timestamp.AddMilliseconds(-1);

            Assert.IsTrue(new AscendingSortedGuid(timestamp, guid) <= new AscendingSortedGuid(timestamp, guid));
            Assert.IsTrue(new AscendingSortedGuid(smallerTimestamp, guid) <= new AscendingSortedGuid(timestamp, guid));
        }

        [Test]
        public void AscendingSortedGuid_OperatorOverload_LessOrEquals_Returns_False()
        {
            var guid = Guid.NewGuid();
            var timestamp = DateTimeOffset.UtcNow;
            var biggerTimestamp = timestamp.AddMilliseconds(1);

            Assert.IsFalse(new AscendingSortedGuid(biggerTimestamp, guid) <= new AscendingSortedGuid(timestamp, guid));
        }

        [Test]
        public void AscendingSortedGuid_OperatorOverload_MoreOrEquals_Returns_True()
        {
            var guid = Guid.NewGuid();
            var timestamp = DateTimeOffset.UtcNow;
            var smallerTimestamp = timestamp.AddMilliseconds(-1);

            Assert.IsTrue(new AscendingSortedGuid(timestamp, guid) >= new AscendingSortedGuid(timestamp, guid));
            Assert.IsTrue(new AscendingSortedGuid(timestamp, guid) >= new AscendingSortedGuid(smallerTimestamp, guid));
        }

        [Test]
        public void AscendingSortedGuid_OperatorOverload_MoreOrEquals_Returns_False()
        {
            var guid = Guid.NewGuid();
            var timestamp = DateTimeOffset.UtcNow;
            var biggerTimestamp = timestamp.AddMilliseconds(1);

            Assert.IsFalse(new AscendingSortedGuid(timestamp, guid) >= new AscendingSortedGuid(biggerTimestamp, guid));
        }

        [Test]
        public void AscendingSortedGuid_OperatorOverload_StriclyLessThan_Returns_True()
        {
            var guid = Guid.NewGuid();
            var timestamp = DateTimeOffset.UtcNow;
            var smallerTimestamp = timestamp.AddMilliseconds(-1);

            Assert.IsTrue(new AscendingSortedGuid(smallerTimestamp, guid) < new AscendingSortedGuid(timestamp, guid));
        }

        [Test]
        public void AscendingSortedGuid_OperatorOverload_StriclyLessThan_Returns_False()
        {
            var guid = Guid.NewGuid();
            var timestamp = DateTimeOffset.UtcNow;

            Assert.IsFalse(new AscendingSortedGuid(timestamp, guid) < new AscendingSortedGuid(timestamp, guid));
        }

        [Test]
        public void AscendingSortedGuid_OperatorOverload_StriclyMoreThan_Returns_True()
        {
            var guid = Guid.NewGuid();
            var timestamp = DateTimeOffset.UtcNow;
            var smallerTimestamp = timestamp.AddMilliseconds(-1);

            Assert.IsTrue(new AscendingSortedGuid(timestamp, guid) > new AscendingSortedGuid(smallerTimestamp, guid));
        }

        [Test]
        public void AscendingSortedGuid_OperatorOverload_StriclyMoreThan_Returns_False()
        {
            var guid = Guid.NewGuid();
            var timestamp = DateTimeOffset.UtcNow;

            Assert.IsFalse(new AscendingSortedGuid(timestamp, guid) > new AscendingSortedGuid(timestamp, guid));
        }

        [Test]
        public void AscendingSortedGuid_OperatorOverload_DifferentThan_Returns_True()
        {
            var guid = Guid.NewGuid();
            var timestamp = DateTimeOffset.UtcNow;
            var smallerTimestamp = timestamp.AddMilliseconds(-1);

            Assert.IsTrue(new AscendingSortedGuid(timestamp, guid) != new AscendingSortedGuid(smallerTimestamp, guid));
            Assert.IsTrue(new AscendingSortedGuid(smallerTimestamp, guid) != new AscendingSortedGuid(timestamp, guid));
            Assert.IsTrue(AscendingSortedGuid.NewSortedGuid() != AscendingSortedGuid.NewSortedGuid());
        }

        [Test]
        public void AscendingSortedGuid_OperatorOverload_DifferentThan_Returns_False()
        {
            var guid = Guid.NewGuid();
            var timestamp = DateTimeOffset.UtcNow;
            var empty1 = AscendingSortedGuid.Empty;
            var empty2 = AscendingSortedGuid.Empty;

            Assert.IsFalse(new AscendingSortedGuid(timestamp, guid) != new AscendingSortedGuid(timestamp, guid));
            Assert.IsFalse(new AscendingSortedGuid() != new AscendingSortedGuid());
            Assert.IsFalse(empty1 != empty2);
        }

        [Test]
        public void AscendingSortedGUid_CompareToObject_With_NullArgument_Returns_MinusOne()
        {
            object obj = null;
            Assert.IsTrue(new AscendingSortedGuid().CompareTo(obj) == -1);
        }

        [Test]
        public void AscendingSortedGUid_CompareToObject_With_NotAssignableType_Returns_MinusOne()
        {
            object obj = new { value = "value" };
            Assert.IsTrue(new AscendingSortedGuid().CompareTo(obj) == -1);
        }

        [Test]
        public void AscendingSortedGuid_CompareToObject_Returns_Correct_Comparison_Value()
        {
            var guid = Guid.NewGuid();
            var timestamp = DateTimeOffset.UtcNow;
            var smallerTimestamp = timestamp.AddMilliseconds(-1);

            Assert.IsTrue(new AscendingSortedGuid(timestamp, guid).CompareTo((object)new AscendingSortedGuid(smallerTimestamp, guid)) == 1);
            Assert.IsTrue(new AscendingSortedGuid(smallerTimestamp, guid).CompareTo((object)new AscendingSortedGuid(timestamp, guid)) == -1);
            Assert.IsTrue(new AscendingSortedGuid(timestamp, guid).CompareTo((object)new AscendingSortedGuid(timestamp, guid)) == 0);
        }


        private static Fake<AscendingSortedGuid> NewItemAsc()
        {
            Fake<AscendingSortedGuid> item = new Fake<AscendingSortedGuid>();
            item.Id = AscendingSortedGuid.NewSortedGuid();
            item.Name = Randomizer.GetRandomString();
            return item;
        }

        private static void AssertCompare(AscendingSortedGuid expected, AscendingSortedGuid actual)
        {
            Assert.AreEqual(expected.Guid, actual.Guid);
            Assert.AreEqual(expected.Timestamp, actual.Timestamp);
        }

        private static void AssertCompare(Fake<AscendingSortedGuid> expected, Fake<AscendingSortedGuid> actual)
        {
            AssertCompare(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
        }

        class Fake<IdType>
        {
            public IdType Id { get; set; }
            public string Name { get; set; }
        }
    }
}

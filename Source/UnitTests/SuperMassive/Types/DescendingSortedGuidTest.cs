using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperMassive.UnitTestHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SuperMassive.Tests
{
    [TestClass]
    public class DescendingGuidTest
    {
        [TestMethod]
        public void CreateDescendingSortedGuid_WithSuccess()
        {
            DescendingSortedGuid idDesc = DescendingSortedGuid.NewSortedGuid();
            Assert.IsNotNull(idDesc);

            Console.WriteLine(idDesc.ToString());
        }

        [TestMethod]
        public void TryParseDescendingSortedGuid_WithSuccess()
        {
            AscendingSortedGuid guid;
            Assert.IsTrue(AscendingSortedGuid.TryParse("0635318522499400050_B77AD6F9624A4C2896E8545923E56502", out guid));
            Assert.AreEqual("0635318522499400050_b77ad6f9624a4c2896e8545923e56502", guid.ToString());
        }

        [TestMethod]
        public void ParseDescendingSortedGuid_WithSuccess()
        {
            DescendingSortedGuid expected = DescendingSortedGuid.NewSortedGuid();
            DescendingSortedGuid actual = DescendingSortedGuid.Parse(expected.ToString());

            AssertCompare(expected, actual);
        }

        [TestMethod]
        public void DescendingSortedGuid_Order_WithSuccess()
        {
            List<Fake<DescendingSortedGuid>> expected = new List<Fake<DescendingSortedGuid>>();

            for (int i = 0; i < 3; i++)
            {
                var item = NewItemDesc();
                expected.Add(item);
                Thread.Sleep(10); // slowing down
            }

            List<Fake<DescendingSortedGuid>> actual = new List<Fake<DescendingSortedGuid>>();

            actual = expected.OrderByDescending(x => x.Id.ToString()).ToList();
            CommonComparers.AreCollectionEquals(expected, actual, AssertCompare);

            actual = expected.OrderByDescending(x => x.Id).ToList();
            CommonComparers.AreCollectionEquals(expected, actual, AssertCompare);
        }

        [TestMethod]
        public void DescendingSortedGuid_CompareTo_WithSuccess()
        {
            DescendingSortedGuid expected = DescendingSortedGuid.Empty;
            DescendingSortedGuid actual = new DescendingSortedGuid(DateTime.UtcNow, Guid.NewGuid());
            Assert.IsTrue(expected.CompareTo(actual) == 1);

            expected = new DescendingSortedGuid(DateTime.UtcNow.AddDays(1), Guid.NewGuid());
            actual = new DescendingSortedGuid(DateTime.UtcNow, Guid.NewGuid());
            Assert.IsTrue(expected.CompareTo(actual) == -1);

            expected = DescendingSortedGuid.Empty;
            actual = DescendingSortedGuid.Empty;
            Assert.IsTrue(expected.CompareTo(actual) == 0);
        }

        private static Fake<DescendingSortedGuid> NewItemDesc()
        {
            Fake<DescendingSortedGuid> item = new Fake<DescendingSortedGuid>();
            item.Id = DescendingSortedGuid.NewSortedGuid();
            item.Name = Randomizer.RandomString();
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
        class Fake<IdType>
        {
            public IdType Id { get; set; }
            public string Name { get; set; }
        }
    }
}

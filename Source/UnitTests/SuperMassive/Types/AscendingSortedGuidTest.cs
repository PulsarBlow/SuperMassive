using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperMassive.UnitTestHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SuperMassive.Tests
{
    [TestClass]
    public class AscendingGuidTest
    {
        [TestMethod]
        public void CreateAscendingSortedGuid_WithSuccess()
        {
            AscendingSortedGuid idAsc = AscendingSortedGuid.NewSortedGuid();
            Assert.IsNotNull(idAsc);

            Console.WriteLine(idAsc.ToString());

            DescendingSortedGuid idDesc = DescendingSortedGuid.NewSortedGuid();
            Assert.IsNotNull(idDesc);

            Console.WriteLine(idDesc.ToString());
        }

        [TestMethod]
        public void TryParseAscendingSortedGuid_WithSuccess()
        {
            AscendingSortedGuid guid;
            Assert.IsTrue(AscendingSortedGuid.TryParse("0635318522499400050_B77AD6F9624A4C2896E8545923E56502", out guid));
            Assert.AreEqual("0635318522499400050_b77ad6f9624a4c2896e8545923e56502", guid.ToString());
        }

        [TestMethod]
        public void ParseAscendingSortedGuid_WithSuccess()
        {
            AscendingSortedGuid expected = AscendingSortedGuid.NewSortedGuid();
            AscendingSortedGuid actual = AscendingSortedGuid.Parse(expected.ToString());

            AssertCompare(expected, actual);
        }

        [TestMethod]
        public void AscendingSortedGuid_Order_WithSuccess()
        {
            List<Fake<AscendingSortedGuid>> expected = new List<Fake<AscendingSortedGuid>>();

            for (int i = 0; i < 3; i++)
            {
                var item = NewItemAsc();
                expected.Add(item);
                Thread.Sleep(10); // slowing down                
            }

            List<Fake<AscendingSortedGuid>> actual = new List<Fake<AscendingSortedGuid>>();

            actual = expected.OrderBy(x => x.Id.ToString()).ToList();
            CommonComparers.AreCollectionEquals(expected, actual, AssertCompare);

            actual = expected.OrderBy(x => x.Id).ToList();
            CommonComparers.AreCollectionEquals(expected, actual, AssertCompare);

        }

        private static Fake<AscendingSortedGuid> NewItemAsc()
        {
            Fake<AscendingSortedGuid> item = new Fake<AscendingSortedGuid>();
            item.Id = AscendingSortedGuid.NewSortedGuid();
            item.Name = Randomizer.RandomString();
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

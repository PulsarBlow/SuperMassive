using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SuperMassive.Tests
{
    [TestClass]
    public class RandomizerTest
    {
        [TestMethod]
        public void RandomStringTest()
        {
            Assert.IsFalse(String.IsNullOrWhiteSpace(Randomizer.RandomString()));
            Assert.AreEqual(64, Randomizer.RandomString().Length);
            Assert.AreEqual(10, Randomizer.RandomString(10).Length);
            Assert.AreEqual(1, Randomizer.RandomString(1).Length);
            Assert.AreEqual(200, Randomizer.RandomString(200).Length);
        }

    }
}

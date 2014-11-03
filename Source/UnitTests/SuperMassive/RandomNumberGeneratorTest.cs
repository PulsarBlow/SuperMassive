using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace SuperMassive.Tests
{
    [TestClass]
    public class RandomNumberGeneratorTest
    {
        [TestMethod]
        public void IntSequenceTest()
        {
            Assert.IsTrue(RandomNumberGenerator.IntSequence(4).Count() == 4);
        }
    }
}

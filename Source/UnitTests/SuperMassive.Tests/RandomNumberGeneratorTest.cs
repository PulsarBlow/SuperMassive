using System.Linq;
using NUnit.Framework;

namespace SuperMassive.Tests
{
    public class RandomNumberGeneratorTest
    {
        [Test]
        public void IntSequenceTest()
        {
            Assert.IsTrue(RandomNumberGenerator.IntSequence(4).Count() == 4);
        }
    }
}

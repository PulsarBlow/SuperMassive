namespace SuperMassive.Tests.Unit
{
    using System.Linq;
    using NUnit.Framework;

    public class RandomNumberGeneratorTest
    {
        [Test]
        public void IntSequenceTest()
        {
            Assert.IsTrue(RandomNumberGenerator.IntSequence(4).Count() == 4);
        }
    }
}

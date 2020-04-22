namespace SuperMassive.Tests.Unit.Extensions
{
    using System;
    using NUnit.Framework;
    using SuperMassive.Extensions;

    public class RandomExtensionsTest
    {
        [Test]
        public void RandomExtensions_NextInt32_Returns_Int32()
        {
            Assert.That(new Random().NextInt32(), Is.AtLeast(int.MinValue).And.AtMost(int.MaxValue));
        }

        [Test]
        public void RandomExtensions_NextDecimal_Returns_Decimal()
        {
            var rnd = new Random();
            for (int i = 0; i < 1000; i++)
            {
                Assert.That(new Random().NextDecimal(), Is.AtLeast(decimal.MinValue).And.AtMost(decimal.MaxValue));
            }
        }

        [Test]
        public void RandomExtensions_NextDecimal_Returns_Positive_Decimal()
        {
            var rnd = new Random();
            for (int i = 0; i < 1000; i++)
            {
                Assert.That(new Random().NextDecimal(true), Is.AtLeast(0).And.AtMost(decimal.MaxValue));
            }
        }
    }
}

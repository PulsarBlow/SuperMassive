namespace SuperMassive.Tests
{
    using NUnit.Framework;
    using SuperMassive.Extensions;

    public class DoubleExtensionsTest
    {
        public void DoubleExtensions_AlmostEquals_WithVariousValues_Returns_True()
        {
            Assert.IsTrue(0d.AlmostEquals(0d));
            Assert.IsTrue(0d.AlmostEquals(0f));
            Assert.IsTrue(0d.AlmostEquals(0));
            Assert.IsTrue(1.0000001d.AlmostEquals(1.0000001f));
            Assert.IsTrue(0.0000001d.AlmostEquals(0));
        }

        [Test]
        public void DoubleExtensions_AlmostEquals_WithVariousValues_Returns_False()
        {
            Assert.IsFalse((1 / 3d).AlmostEquals(0.333d));
            Assert.IsFalse(1d.AlmostEquals(0f));
            Assert.IsFalse(0.0001d.AlmostEquals(0.1, 0.001d));
        }
    }
}

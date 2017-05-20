namespace SuperMassive.Tests
{
    using System.Linq;
    using NUnit.Framework;
    using SuperMassive.Extensions;

    public class IntegerExtensionsTest
    {
        [TestCase(short.MinValue)]
        [TestCase(0)]
        [TestCase(short.MaxValue)]
        public void IntegerExtensions_AsMask_WithInt16_Returns_BitwiseMask(short value)
        {
            var result = value.AsMask();
            Assert.That(result, Is.AssignableFrom(typeof(BitwiseMask)));
        }

        [TestCase(int.MinValue)]
        [TestCase(0)]
        [TestCase(int.MaxValue)]
        public void IntegerExtensions_AsMask_WithInt32_Returns_BitwiseMask(int value)
        {
            var result = value.AsMask();
            Assert.That(result, Is.AssignableFrom(typeof(BitwiseMask)));
        }

        [TestCase(long.MinValue)]
        [TestCase(0)]
        [TestCase(long.MaxValue)]
        public void IntegerExtensions_AsMask_WithInt64_Returns_BitwiseMask(long value)
        {
            var result = value.AsMask();
            Assert.That(result, Is.AssignableFrom(typeof(BitwiseMask)));
        }

        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(-1, 1)]
        [TestCase(-10, 1)]
        [TestCase(10, -1)]
        [TestCase(int.MinValue, int.MinValue)]
        [TestCase(int.MaxValue, int.MaxValue)]
        [TestCase(int.MinValue, int.MaxValue)]
        [TestCase(int.MaxValue, int.MinValue)]
        public void IntegerExtensions_To_Returns_Valid_Sequence(int startingValue, int endingValue)
        {
            var result = startingValue.To(endingValue);

            Assert.That(result.Count() > 0);
        }
    }
}

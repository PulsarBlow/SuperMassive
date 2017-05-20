namespace SuperMassive.Tests
{
    using System;
    using NUnit.Framework;
    using SuperMassive.Extensions;

    public class TypeExtensionsTest
    {
        [Test]
        public void TypeExtensions_IsNullable_With_Null_Argument_Returns_False()
        {
            var type = typeof(int?);
            type = null;
            Assert.IsFalse(type.IsNullable());
        }

        [TestCase(typeof(string))]
        [TestCase(typeof(int))]
        [TestCase(typeof(long))]
        [TestCase(typeof(DateTime))]
        public void TypeExtensions_IsNullable_With_NonNullableType_Returns_False(Type type)
        {
            Assert.IsFalse(type.IsNullable());
        }

        [TestCase(typeof(int?))]
        [TestCase(typeof(long?))]
        [TestCase(typeof(DateTime?))]
        public void TypeExtensions_IsNullable_With_NullableType_Returns_True(Type type)
        {
            Assert.IsTrue(type.IsNullable());
        }
    }
}

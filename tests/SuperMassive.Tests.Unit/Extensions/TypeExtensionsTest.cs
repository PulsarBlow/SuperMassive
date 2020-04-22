namespace SuperMassive.Tests.Unit.Extensions
{
    using System;
    using NUnit.Framework;
    using SuperMassive.Extensions;

    public class TypeExtensionsTest
    {
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

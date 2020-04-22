namespace SuperMassive.Tests.Unit.Extensions
{
    using System;
    using NUnit.Framework;
    using SuperMassive.Extensions;

    public class EnumExtensionsTest
    {
        [TestCase(TestEnum.None, TestEnum.Something, 1)]
        [TestCase(TestEnum.Something, TestEnum.SomethingElse, 3)]
        [TestCase(TestEnum.None, TestEnum.None, 0)]
        [TestCase(TestEnum.Something, TestEnum.Something, 1)]
        public void EnumExtensions_Include_Includes(TestEnum enumValue1, TestEnum enumValue2, int expected)
        {
            Assert.AreEqual(expected, (int) enumValue1.Include(enumValue2));
        }

        [TestCase(TestUnsignedEnum.None, TestUnsignedEnum.Something, 1u)]
        [TestCase(TestUnsignedEnum.Something, TestUnsignedEnum.SomethingElse, 3u)]
        [TestCase(TestUnsignedEnum.None, TestUnsignedEnum.None, 0u)]
        [TestCase(TestUnsignedEnum.Something, TestUnsignedEnum.Something, 1u)]
        public void EnumExtensions_Include_Includes_Unsigned(TestUnsignedEnum unsignedEnumValue1,
            TestUnsignedEnum unsignedEnumValue2, uint expected)
        {
            Assert.AreEqual(expected, (int) unsignedEnumValue1.Include(unsignedEnumValue2));
        }

        [Test]
        public void EnumExtensions_Include_Includes_Flagged()
        {
            const TestFlaggedEnum @enum = TestFlaggedEnum.Value1;
            const int expected = 3;
            Assert.AreEqual(expected, (int) @enum.Include(TestFlaggedEnum.Value2));
        }

        [TestCase(TestEnum.None, TestEnum.None, 0)]
        [TestCase(TestEnum.Something, TestEnum.None, 1)]
        [TestCase(TestEnum.SomethingElse, TestEnum.Something, 2)]
        public void EnumExtensions_Remove_Removes(TestEnum enum1, TestEnum enum2, int expected)
        {
            Assert.AreEqual(expected, (int) enum1.Remove(enum2));
        }

        [TestCase(TestEnum.Something, TestEnum.Something, true)]
        [TestCase(TestEnum.Something, TestEnum.SomethingElse, false)]
        public void EnumExtensions_Has_Returns_Expected(TestEnum @enum, TestEnum test, bool expected)
        {
            Assert.AreEqual(expected, @enum.Has(test));
        }

        [TestCase(TestFlaggedEnum.Value1, TestFlaggedEnum.Value1, true)]
        [TestCase(TestFlaggedEnum.Value1 | TestFlaggedEnum.Value5, TestFlaggedEnum.Value5, true)]
        [TestCase(TestFlaggedEnum.Value1 | TestFlaggedEnum.Value5, TestFlaggedEnum.Value2, false)]
        public void EnumExtensions_Has_Flagged_Returns_Expected(TestFlaggedEnum @enum, TestFlaggedEnum test,
            bool expected)
        {
            Assert.AreEqual(expected, @enum.Has(test));
        }

        [TestCase(TestEnum.Something, TestEnum.SomethingElse, true)]
        [TestCase(TestEnum.Something, TestEnum.Something, false)]
        public void EnumExtensions_Missing_Returns_Expected(TestEnum @enum, TestEnum test, bool expected)
        {
            Assert.AreEqual(expected, @enum.Missing(test));
        }

        [TestCase(TestFlaggedEnum.Value1, TestFlaggedEnum.Value1, false)]
        [TestCase(TestFlaggedEnum.Value1 | TestFlaggedEnum.Value5, TestFlaggedEnum.Value5, false)]
        [TestCase(TestFlaggedEnum.Value1 | TestFlaggedEnum.Value5, TestFlaggedEnum.Value2, true)]
        public void EnumExtensions_Missing_Flagged_Returns_True(TestFlaggedEnum @enum, TestFlaggedEnum test,
            bool expected)
        {
            Assert.AreEqual(expected, @enum.Missing(test));
        }

        public enum TestEnum
        {
            None,
            Something,
            SomethingElse,
        }

        public enum TestUnsignedEnum : ushort
        {
            None,
            Something,
            SomethingElse,
        }

        [Flags]
        public enum TestFlaggedEnum
        {
            NotSet = 0,
            Value1 = 1,
            Value2 = 2,
            Value3 = 4,
            Value4 = 8,
            Value5 = 16
        }
    }
}

using System;
using NUnit.Framework;

namespace SuperMassive.Tests
{
    public class EnumerationExtensionsTest
    {
        [Flags]
        public enum DummyEnum
        {
            NotSet = 0,
            Value1 = 1,
            Value2 = 2,
            Value3 = 4,
            Value4 = 8,
            Value5 = 16
        }

        [Test]
        public void AllMethodsTest()
        {
            DummyEnum dummy = DummyEnum.NotSet;
            dummy = dummy.Include(DummyEnum.Value1 | DummyEnum.Value2 | DummyEnum.Value3);
            Assert.IsTrue(dummy.Has(DummyEnum.Value1));
            Assert.IsTrue(dummy.Has(DummyEnum.Value2));
            Assert.IsTrue(dummy.Has(DummyEnum.Value3));
            Assert.IsTrue(dummy.Missing(DummyEnum.Value4));
            dummy = dummy.Remove(DummyEnum.Value1);
            Assert.IsTrue(dummy.Missing(DummyEnum.Value1));
            dummy = dummy.Remove(DummyEnum.Value2 | DummyEnum.Value3);
            Assert.IsTrue(dummy == DummyEnum.NotSet);
        }
    }
}

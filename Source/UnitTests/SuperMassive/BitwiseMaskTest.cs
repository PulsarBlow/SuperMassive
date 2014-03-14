using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperMassive;

namespace SuperMassiveTests
{
    [TestClass]
    public class BitwiseMaskTest
    {
        [TestMethod]
        public void Has()
        {
            short valueAsShort = 4;
            int valueAsInt = 4;
            long valueAsLong = 4;

            BitwiseMask flag = new BitwiseMask(valueAsShort);
            Assert.IsTrue(flag.Has(valueAsShort));
            flag.Remove(valueAsShort);
            Assert.IsFalse(flag.Has(valueAsShort));

            flag = new BitwiseMask(valueAsInt);
            Assert.IsTrue(flag.Has(valueAsInt));
            flag.Remove(valueAsInt);
            Assert.IsFalse(flag.Has(valueAsInt));

            flag = new BitwiseMask(valueAsLong);
            Assert.IsTrue(flag.Has(valueAsLong));
            flag.Remove(valueAsLong);
            Assert.IsFalse(flag.Has(valueAsLong));

            flag = new BitwiseMask(4);
            Assert.IsFalse(flag.IsEmpty());
            flag.Remove(4);
            Assert.IsTrue(flag.IsEmpty());
            flag.Add(8).Add(16).Add(32);
            Assert.IsTrue(flag.Has(8));
            Assert.IsTrue(flag.Has(32));
            flag.Remove(32);
            Assert.IsFalse(flag.Has(32));

            flag = new BitwiseMask();
            flag.Add(4).Add(8).Add(16);
            Assert.IsTrue(flag.Is((4 | 8 | 16)));
            Assert.IsFalse(flag.Is(256));
        }
    }
}

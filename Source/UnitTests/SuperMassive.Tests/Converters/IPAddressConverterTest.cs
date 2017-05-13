using NUnit.Framework;

namespace SuperMassive.Tests
{
    public class IPAddressConverterTest
    {
        [Test]
        public void ToInt64Test()
        {
            Assert.AreEqual(1474760558, IPAddressConverter.ToInt64("87.231.15.110"));
            Assert.AreEqual(2130706433, IPAddressConverter.ToInt64("127.0.0.1"));
        }
    }
}

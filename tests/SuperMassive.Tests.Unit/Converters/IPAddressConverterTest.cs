namespace SuperMassive.Tests.Unit.Converters
{
    using NUnit.Framework;

    public class IPpAddressConverterTest
    {
        [Test]
        public void ToInt64Test()
        {
            Assert.AreEqual(1474760558, IpAddressConverter.ToInt64("87.231.15.110"));
            Assert.AreEqual(2130706433, IpAddressConverter.ToInt64("127.0.0.1"));
        }
    }
}

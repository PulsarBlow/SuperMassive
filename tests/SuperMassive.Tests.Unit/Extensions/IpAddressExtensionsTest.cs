namespace SuperMassive.Tests.Unit.Extensions
{
    using System.Net;
    using NUnit.Framework;
    using SuperMassive.Extensions;

    public class IpAddressExtensionsTest
    {
        [TestCase("0.0.0.0", 0)]
        [TestCase("255.255.255.255", 4294967295)]
        public void IpAddressExtensions_ToLongValue_Returns_Long(string actual, long expected)
        {
            Assert.That(IPAddress.Parse(actual).ToLongValue(), Is.EqualTo(expected));
        }
    }
}

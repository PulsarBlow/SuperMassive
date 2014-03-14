using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperMassive;
using System;

namespace SuperMassiveTests
{
    [TestClass]
    public class RegexHelperTest
    {
        [TestMethod]
        public void IsSHA256Test()
        {
            Assert.IsTrue(RegexHelper.IsSHA256Hash(CryptographyHelper.ComputeSHA256Hash(Guid.Empty.ToString())));
            Assert.IsTrue(RegexHelper.IsSHA256Hash(CryptographyHelper.ComputeSHA256Hash(Guid.NewGuid().ToString())));
            Assert.IsFalse(RegexHelper.IsSHA256Hash(CryptographyHelper.ComputeSHA256Hash(Guid.NewGuid().ToString()).Substring(0, 31)));
            Assert.IsFalse(RegexHelper.IsSHA256Hash(CryptographyHelper.ComputeSHA256Hash(Guid.NewGuid().ToString()).Substring(0, 63) + "@"));
        }
        [TestMethod]
        public void IsSecretKeyTest()
        {
            Assert.IsTrue(RegexHelper.IsSHA256Hash(CryptographyHelper.ComputeSHA256Hash(Guid.Empty.ToString())));
            Assert.IsTrue(RegexHelper.IsSHA256Hash(CryptographyHelper.ComputeSHA256Hash(Guid.NewGuid().ToString())));
            Assert.IsFalse(RegexHelper.IsSHA256Hash(CryptographyHelper.ComputeSHA256Hash(Guid.NewGuid().ToString()).Substring(0, 12)));
            Assert.IsFalse(RegexHelper.IsSHA256Hash(CryptographyHelper.ComputeSHA256Hash(Guid.NewGuid().ToString()).Substring(0, 15) + "@"));
        }
        [TestMethod]
        public void IsEmailTest()
        {
            Assert.IsTrue(RegexHelper.IsEmail("b172f15bac94d30fde90@example.com"));

        }
        [TestMethod]
        public void IsUrlTest()
        {
            Assert.IsTrue(RegexHelper.IsUrl("http://www.domain.com/page.html"));
            Assert.IsTrue(RegexHelper.IsUrl("https://www.domain.com/page.html"));
            Assert.IsTrue(RegexHelper.IsUrl("ftp://www.domain.com/page.html"));
            Assert.IsTrue(RegexHelper.IsUrl("http://domain.com/?query=a"));
            Assert.IsTrue(RegexHelper.IsUrl("http://www.domain.com/page#result"));
            Assert.IsTrue(RegexHelper.IsUrl("http://www.domain.com/page.html/"));
            Assert.IsTrue(RegexHelper.IsUrl("http://subdomain.domain.com/page.html"));
            Assert.IsTrue(RegexHelper.IsUrl("http://www.domain.com/1234,345"));
            Assert.IsTrue(RegexHelper.IsUrl("http://127.0.0.1:8090/"));

            Assert.IsFalse(RegexHelper.IsUrl("xtp://domain.com"));
            Assert.IsFalse(RegexHelper.IsUrl("http://localhost/")); // Should use 127.0.0.1
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SuperMassive.Fakers.Tests
{
    [TestClass]
    public class InternetTest
    {
        [TestMethod]
        public void StaticFieldsInitialization()
        {
            var privateInternet = new PrivateType(typeof(Internet));
            Assert.IsNotNull(privateInternet.GetStaticField("BYTE"));
            Assert.IsNotNull(privateInternet.GetStaticField("HOSTS"));
            Assert.IsNotNull(privateInternet.GetStaticField("DISPOSABLE_HOSTS"));
            Assert.IsNotNull(privateInternet.GetStaticField("DOMAIN_SUFFIXES"));
            Assert.IsNotNull(privateInternet.GetStaticField("IMAGE_SUFFIXES"));
        }

        [TestMethod]
        public void EmailTest_Without_Args()
        {
            string result = Internet.Email();
            Assert.IsFalse(String.IsNullOrEmpty(result));
            Assert.IsFalse(String.IsNullOrWhiteSpace(result));
            Assert.IsTrue(result.Contains("@"));
        }
        [TestMethod]
        public void EmailTest_With_Args()
        {
            string arg = Lorem.Word();
            string result = Internet.Email(arg);
            Assert.IsFalse(String.IsNullOrEmpty(result));
            Assert.IsFalse(String.IsNullOrWhiteSpace(result));
            Assert.IsTrue(result.Contains("@"));
            Assert.IsTrue(result.Contains(arg));
        }
        [TestMethod]
        public void UserNameTest_Without_Args()
        {
            string result = Internet.UserName();
            Assert.IsFalse(String.IsNullOrEmpty(result));
            Assert.IsFalse(String.IsNullOrWhiteSpace(result));
        }
        [TestMethod]
        public void UserNameTest_With_Args()
        {
            string arg = Lorem.Word();
            string result = Internet.UserName(arg);
            Assert.IsFalse(String.IsNullOrEmpty(result));
            Assert.IsFalse(String.IsNullOrWhiteSpace(result));
            Assert.IsTrue(result.Contains(arg));
        }

        [TestMethod]
        public void DomainNameTest()
        {
            string result = Internet.DomainName();
            Assert.IsFalse(String.IsNullOrEmpty(result));
            Assert.IsFalse(String.IsNullOrWhiteSpace(result));
            Assert.IsTrue(result.Contains("."));
        }
        [TestMethod]
        public void DomainWordTest()
        {
            string result = Internet.DomainWord();
            Assert.IsFalse(String.IsNullOrEmpty(result));
            Assert.IsFalse(String.IsNullOrWhiteSpace(result));
        }
        [TestMethod]
        public void DomainSuffixTest()
        {
            string result = Internet.DomainSuffix();
            Assert.IsFalse(String.IsNullOrEmpty(result));
            Assert.IsFalse(String.IsNullOrWhiteSpace(result));
        }
        [TestMethod]
        public void UriTest_With_Args()
        {
            string arg = Lorem.Word();
            string result = Internet.Uri(arg);
            Assert.IsFalse(String.IsNullOrEmpty(result));
            Assert.IsFalse(String.IsNullOrWhiteSpace(result));
            Assert.IsTrue(result.Contains(arg));
            Assert.IsTrue(result.Contains("://"));
        }
        [TestMethod]
        public void ImageUrlTest()
        {
            string result = Internet.ImageUrl();
            Assert.IsFalse(String.IsNullOrEmpty(result));
            Assert.IsFalse(String.IsNullOrWhiteSpace(result));
        }
        [TestMethod]
        public void IP_V4_AddressTest()
        {
            string result = Internet.IP_V4_Address();
            Assert.IsFalse(String.IsNullOrEmpty(result));
            Assert.IsFalse(String.IsNullOrWhiteSpace(result));
        }
    }
}

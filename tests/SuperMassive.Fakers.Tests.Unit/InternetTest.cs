namespace SuperMassive.Fakers.Tests.Units
{
    using System;
    using NUnit.Framework;

    public class InternetTest
    {
        [Test]
        public void EmailTest_Without_Args()
        {
            string result = Internet.Email();
            Assert.False(String.IsNullOrEmpty(result));
            Assert.False(String.IsNullOrWhiteSpace(result));
            Assert.True(result.Contains("@"));
        }
        [Test]
        public void EmailTest_With_Args()
        {
            string arg = Lorem.Word();
            string result = Internet.Email(arg);
            Assert.False(String.IsNullOrEmpty(result));
            Assert.False(String.IsNullOrWhiteSpace(result));
            Assert.True(result.Contains("@"));
            Assert.True(result.Contains(arg));
        }
        [Test]
        public void UserNameTest_Without_Args()
        {
            string result = Internet.UserName();
            Assert.False(String.IsNullOrEmpty(result));
            Assert.False(String.IsNullOrWhiteSpace(result));
        }
        [Test]
        public void UserNameTest_With_Args()
        {
            string arg = Lorem.Word();
            string result = Internet.UserName(arg);
            Assert.False(String.IsNullOrEmpty(result));
            Assert.False(String.IsNullOrWhiteSpace(result));
            Assert.True(result.Contains(arg));
        }

        [Test]
        public void DomainNameTest()
        {
            string result = Internet.DomainName();
            Assert.False(String.IsNullOrEmpty(result));
            Assert.False(String.IsNullOrWhiteSpace(result));
            Assert.True(result.Contains("."));
        }
        [Test]
        public void DomainWordTest()
        {
            string result = Internet.DomainWord();
            Assert.False(String.IsNullOrEmpty(result));
            Assert.False(String.IsNullOrWhiteSpace(result));
        }
        [Test]
        public void DomainSuffixTest()
        {
            string result = Internet.DomainSuffix();
            Assert.False(String.IsNullOrEmpty(result));
            Assert.False(String.IsNullOrWhiteSpace(result));
        }
        [Test]
        public void UriTest_With_Args()
        {
            string arg = Lorem.Word();
            string result = Internet.Uri(arg);
            Assert.False(String.IsNullOrEmpty(result));
            Assert.False(String.IsNullOrWhiteSpace(result));
            Assert.True(result.Contains(arg));
            Assert.True(result.Contains("://"));
        }
        [Test]
        public void ImageUrlTest()
        {
            string result = Internet.ImageUrl();
            Assert.False(String.IsNullOrEmpty(result));
            Assert.False(String.IsNullOrWhiteSpace(result));
        }
        [Test]
        public void IP_V4_AddressTest()
        {
            string result = Internet.IP_V4_Address();
            Assert.False(String.IsNullOrEmpty(result));
            Assert.False(String.IsNullOrWhiteSpace(result));
        }
    }
}

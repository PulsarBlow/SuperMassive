using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SuperMassive.Tests
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
            Assert.IsTrue(RegexHelper.IsEmail("b172f15bac94d30fde90@gmail.com"));
            Assert.IsTrue(RegexHelper.IsEmail("b172f15bac94d30fde90+365@gmail.com"));
            Assert.IsTrue(RegexHelper.IsEmail("b172f15ba.c94d30fde90+365@gmail.com"));

            Assert.IsFalse(RegexHelper.IsEmail("@gmail.com"));
            Assert.IsFalse(RegexHelper.IsEmail("c94d30fde90@gmail."));
            Assert.IsFalse(RegexHelper.IsEmail("c94d30fde90@"));
        }

        [TestMethod]
        public void IsCRC32Test()
        {
            Assert.IsTrue(RegexHelper.IsCRC32Hash(CryptographyHelper.ComputeCRC32Hash(Guid.Empty.ToString())));
            Assert.IsTrue(RegexHelper.IsCRC32Hash(CryptographyHelper.ComputeCRC32Hash(Guid.NewGuid().ToString())));

            Assert.IsFalse(RegexHelper.IsCRC32Hash("aaaaaaaaaa"));
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

        [TestMethod]
        public void IsValidTableContainerName()
        {
            Assert.IsFalse(RegexHelper.IsTableContainerNameValid(" 456"));
            Assert.IsFalse(RegexHelper.IsTableContainerNameValid(".456"));
            Assert.IsFalse(RegexHelper.IsTableContainerNameValid("ué456"));
            Assert.IsFalse(RegexHelper.IsTableContainerNameValid("ERT-DDD"));
            Assert.IsFalse(RegexHelper.IsTableContainerNameValid("4Name"));

            Assert.IsTrue(RegexHelper.IsTableContainerNameValid("aname1321"));
            Assert.IsTrue(RegexHelper.IsTableContainerNameValid("A1name13"));

            Assert.IsFalse(RegexHelper.IsTableContainerNameValid("78")); // Min Length = 3

            string name64Length = CryptographyHelper.ComputeMD5Hash(Guid.NewGuid().ToString()) +
                CryptographyHelper.ComputeMD5Hash(Guid.NewGuid().ToString());
            Assert.IsFalse(RegexHelper.IsTableContainerNameValid(name64Length)); // MAx Length = 63

        }

        [TestMethod]

        public void IsValidBlobContainerName()
        {
            Assert.IsFalse(RegexHelper.IsBlobContainerNameValid(" 456"));
            Assert.IsFalse(RegexHelper.IsBlobContainerNameValid(".456"));
            Assert.IsFalse(RegexHelper.IsBlobContainerNameValid("ué456"));
            Assert.IsFalse(RegexHelper.IsBlobContainerNameValid("ERT-DDD"));
            Assert.IsFalse(RegexHelper.IsBlobContainerNameValid("4Name"));
            Assert.IsFalse(RegexHelper.IsBlobContainerNameValid("A1name13"));
            Assert.IsFalse(RegexHelper.IsBlobContainerNameValid("78")); // Min Length = 3

            string name64Length = CryptographyHelper.ComputeMD5Hash(Guid.NewGuid().ToString()) +
                CryptographyHelper.ComputeMD5Hash(Guid.NewGuid().ToString());
            Assert.IsFalse(RegexHelper.IsBlobContainerNameValid(name64Length)); // MAx Length = 63

            Assert.IsTrue(RegexHelper.IsBlobContainerNameValid("ert-ddd"));
            Assert.IsTrue(RegexHelper.IsBlobContainerNameValid("aname1321"));
        }

        [TestMethod]
        public void IsGuidTest()
        {
            Assert.IsFalse(RegexHelper.IsGuid("abc"));
            Assert.IsFalse(RegexHelper.IsGuid("123"));
            Assert.IsFalse(RegexHelper.IsGuid("g77ad6f9-624a-4c28-96e8-545923e56502")); //[A-F0-9]
            Assert.IsTrue(RegexHelper.IsGuid("b77ad6f9-624a-4c28-96e8-545923e56502"));
            Assert.IsTrue(RegexHelper.IsGuid("B77AD6F9-624A-4C28-96E8-545923E56502"));
            Assert.IsTrue(RegexHelper.IsGuid("00000000-0000-0000-0000-000000000000"));
        }

        [TestMethod]
        public void IsSortedGuid()
        {
            Assert.IsTrue(RegexHelper.IsSortedGuid("0000000000000000000_00000000000000000000000000000000"));
            Assert.IsTrue(RegexHelper.IsSortedGuid("0635318522499400050_b77ad6f9624a4c2896e8545923e56502"));
            Assert.IsTrue(RegexHelper.IsSortedGuid("0635318522499400050_B77AD6F9624A4C2896E8545923E56502"));
            Assert.IsTrue(RegexHelper.IsSortedGuid("0635318522499400050_00000000000000000000000000000000"));
            Assert.IsTrue(RegexHelper.IsSortedGuid("0635318522499400050_00000000000000000000000000000000"));


            Assert.IsFalse(RegexHelper.IsSortedGuid("63531852249940005_000000000-0000-0000-0000-000000000000"));
            Assert.IsFalse(RegexHelper.IsSortedGuid("635318522499400050_000000000-0000-0000-0000-00000000000"));
            Assert.IsFalse(RegexHelper.IsSortedGuid("635318522499400050_000000000-0000-0000-000-000000000000"));
            Assert.IsFalse(RegexHelper.IsSortedGuid("635318522499400050_00000000000000000000000000000000G"));
        }
    }
}

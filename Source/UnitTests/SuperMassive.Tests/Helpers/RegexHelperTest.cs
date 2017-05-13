using System;
using NUnit.Framework;
using SuperMassive.UnitTestHelpers;

namespace SuperMassive.Tests
{
    public class RegexHelperTest
    {
        [Test]
        public void IsSHA256Test()
        {
            Assert.IsTrue(RegexHelper.IsSHA256Hash(CryptographyHelper.ComputeSHA256Hash(Guid.Empty.ToString())));
            Assert.IsTrue(RegexHelper.IsSHA256Hash(CryptographyHelper.ComputeSHA256Hash(Guid.NewGuid().ToString())));
            Assert.IsFalse(RegexHelper.IsSHA256Hash(CryptographyHelper.ComputeSHA256Hash(Guid.NewGuid().ToString()).Substring(0, 31)));
            Assert.IsFalse(RegexHelper.IsSHA256Hash(CryptographyHelper.ComputeSHA256Hash(Guid.NewGuid().ToString()).Substring(0, 63) + "@"));
        }
        [Test]
        public void IsSecretKeyTest()
        {
            Assert.IsTrue(RegexHelper.IsSHA256Hash(CryptographyHelper.ComputeSHA256Hash(Guid.Empty.ToString())));
            Assert.IsTrue(RegexHelper.IsSHA256Hash(CryptographyHelper.ComputeSHA256Hash(Guid.NewGuid().ToString())));
            Assert.IsFalse(RegexHelper.IsSHA256Hash(CryptographyHelper.ComputeSHA256Hash(Guid.NewGuid().ToString()).Substring(0, 12)));
            Assert.IsFalse(RegexHelper.IsSHA256Hash(CryptographyHelper.ComputeSHA256Hash(Guid.NewGuid().ToString()).Substring(0, 15) + "@"));
        }
        [Test]
        public void IsEmailTest()
        {
            Assert.IsTrue(RegexHelper.IsEmail("b172f15bac94d30fde90@gmail.com"));
            Assert.IsTrue(RegexHelper.IsEmail("b172f15bac94d30fde90+365@gmail.com"));
            Assert.IsTrue(RegexHelper.IsEmail("b172f15ba.c94d30fde90+365@gmail.com"));

            Assert.IsFalse(RegexHelper.IsEmail("@gmail.com"));
            Assert.IsFalse(RegexHelper.IsEmail("c94d30fde90@gmail."));
            Assert.IsFalse(RegexHelper.IsEmail("c94d30fde90@"));
        }

        [Test]
        public void IsCRC32Test()
        {
            Assert.IsTrue(RegexHelper.IsCRC32Hash(CryptographyHelper.ComputeCRC32Hash(Guid.Empty.ToString())));
            Assert.IsTrue(RegexHelper.IsCRC32Hash(CryptographyHelper.ComputeCRC32Hash(Guid.NewGuid().ToString())));

            Assert.IsFalse(RegexHelper.IsCRC32Hash("aaaaaaaaaa"));
        }
        [Test]
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

        [Test]
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

        [Test]

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

        [Test]
        public void IsGuidTest()
        {
            Assert.IsFalse(RegexHelper.IsGuid("abc"));
            Assert.IsFalse(RegexHelper.IsGuid("123"));
            Assert.IsFalse(RegexHelper.IsGuid("g77ad6f9-624a-4c28-96e8-545923e56502")); //[A-F0-9]
            Assert.IsTrue(RegexHelper.IsGuid("b77ad6f9-624a-4c28-96e8-545923e56502"));
            Assert.IsTrue(RegexHelper.IsGuid("B77AD6F9-624A-4C28-96E8-545923E56502"));
            Assert.IsTrue(RegexHelper.IsGuid("00000000-0000-0000-0000-000000000000"));
        }

        [Test]
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

        [Test]
        public void IsSemver()
        {
            ExtendedAssert.IsTrueWithMessage(RegexHelper.IsSemver, "1.0.0-alpha-a.b-c-somethinglong+build.1-aef.1-its-okay");
            ExtendedAssert.IsTrueWithMessage(RegexHelper.IsSemver, "1.0.0+0.build.1-rc.10000aaa-kk-0.1");
            ExtendedAssert.IsTrueWithMessage(RegexHelper.IsSemver, "1.0.0-rc.1+build.1");
            ExtendedAssert.IsTrueWithMessage(RegexHelper.IsSemver, "2.0.0-rc.1+build.123");
            ExtendedAssert.IsTrueWithMessage(RegexHelper.IsSemver, "0.0.4");
            ExtendedAssert.IsTrueWithMessage(RegexHelper.IsSemver, "1.2.3");
            ExtendedAssert.IsTrueWithMessage(RegexHelper.IsSemver, "10.20.30");
            ExtendedAssert.IsTrueWithMessage(RegexHelper.IsSemver, "1.2.3-beta");
            ExtendedAssert.IsTrueWithMessage(RegexHelper.IsSemver, "10.2.3-DEV-SNAPSHOT");
            ExtendedAssert.IsTrueWithMessage(RegexHelper.IsSemver, "1.2.3-SNAPSHOT-123");
            ExtendedAssert.IsTrueWithMessage(RegexHelper.IsSemver, "1.0.0");
            ExtendedAssert.IsTrueWithMessage(RegexHelper.IsSemver, "2.0.0");
            ExtendedAssert.IsTrueWithMessage(RegexHelper.IsSemver, "1.1.7");
            ExtendedAssert.IsTrueWithMessage(RegexHelper.IsSemver, "2.0.0+build.1848");
            ExtendedAssert.IsTrueWithMessage(RegexHelper.IsSemver, "2.0.1-alpha.1227");
            ExtendedAssert.IsTrueWithMessage(RegexHelper.IsSemver, "1.2.3----RC-SNAPSHOT.12.9.1--.12+788");

            ExtendedAssert.IsFalseWithMessage(RegexHelper.IsSemver, "01.1.1");
            ExtendedAssert.IsFalseWithMessage(RegexHelper.IsSemver, "1.2");
            ExtendedAssert.IsFalseWithMessage(RegexHelper.IsSemver, "1.2.3.DEV");
            ExtendedAssert.IsFalseWithMessage(RegexHelper.IsSemver, "1.2-SNAPSHOT");
            ExtendedAssert.IsFalseWithMessage(RegexHelper.IsSemver, "1.2.3----RC-SNAPSHOT.12.09.1--..12+788");
            ExtendedAssert.IsFalseWithMessage(RegexHelper.IsSemver, "1.2-RC-SNAPSHOT");
            ExtendedAssert.IsFalseWithMessage(RegexHelper.IsSemver, "-1.0.3-gamma+b7718");
        }
    }
}

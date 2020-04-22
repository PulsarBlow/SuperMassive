namespace SuperMassive.Tests.Unit.Helpers
{
    using NUnit.Framework;

    public class RegexHelperTest
    {
        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase("something", false)]
        [TestCase("somethingc7d9dc229d2921a40e899ef", false)]
        [TestCase("7215ee9c7d9dc229d2921a40e899ec5f", true)]
        [TestCase("7215EE9C7D9DC229D2921A40E899EC5F", true)]
        public void IsMD5Hash_Returns_True_When_Value_Is_Md5(string value, bool expected)
        {
            Assert.That(RegexHelper.IsMD5Hash(value), Is.EqualTo(expected));
        }

        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase("something", false)]
        [TestCase("something3328aad1201f1dcfead4d5aeb84580fae41157afacc4367f3e8b3d9", false)]
        [TestCase("9296c1a118328aad1201f1dcfead4d5aeb84580fae41157afacc4367f3e8b3d9", true)]
        [TestCase("9296C1A118328AAD1201F1DCFEAD4D5AEB84580FAE41157AFACC4367F3E8B3D9", true)]
        public void IsSHA256Hash_Returns_True_When_Value_Is_Sha256(string value, bool expected)
        {
            Assert.That(RegexHelper.IsSHA256Hash(value), Is.EqualTo(expected));
        }

        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase("somethin", false)]
        [TestCase("aaaaaaaaa", false)]
        [TestCase("e96ccf45", true)]
        [TestCase("93ba819f", true)]
        [TestCase("93BA819F", true)]
        public void IsCRC32Hash_Returns_True_When_Value_Is_Crc32(string value, bool expected)
        {
            Assert.That(RegexHelper.IsCRC32Hash(value), Is.EqualTo(expected));
        }

        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase("@gmail.com", false)]
        [TestCase("c94d30fde90@gmail.", false)]
        [TestCase("c94d30fde90@", false)]
        [TestCase("b172f15bac94d30fde90@gmail.com", true)]
        [TestCase("b172f15bac94d30fde90+365@gmail.com", true)]
        [TestCase("b172f15ba.c94d30fde90+365@gmail.com", true)]
        public void IsEmail_Returns_True_When_Email_Is_Valid(string value, bool expected)
        {
            Assert.That(RegexHelper.IsEmail(value), Is.EqualTo(expected));
        }

        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase("xtp://domain.com", false)]
        [TestCase("http://localhost/", false)] // Should use 127.0.0.1
        [TestCase("http://www.domain.com/page.html", true)]
        [TestCase("http://www.domain.com/page.html/", true)]
        [TestCase("https://www.domain.com/page.html", true)]
        [TestCase("ftp://www.domain.com/page.html", true)]
        [TestCase("http://domain.com/?query=a", true)]
        [TestCase("http://www.domain.com/page#result", true)]
        [TestCase("http://subdomain.domain.com/page.html", true)]
        [TestCase("http://www.domain.com/1234,345", true)]
        [TestCase("http://127.0.0.1:8090/", true)]
        public void IsUrl_Returns_True_When_Url_Is_Valid(string value, bool expected)
        {
            Assert.That(RegexHelper.IsUrl(value), Is.EqualTo(expected));
        }

        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase(" ", false)]
        [TestCase(" 456", false)]
        [TestCase(".456", false)]
        [TestCase("ué456", false)]
        [TestCase("ERT-DDD", false)]
        [TestCase("4Name", false)]
        [TestCase("78", false)] // Min length = 3
        [TestCase("abcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabca", false)] // Max Length = 63
        [TestCase("aname1321", true)]
        [TestCase("A1name13", true)]
        public void IsTableContainerNameValid_Returns_True_When_Name_Is_Valid(string value, bool expected)
        {
            Assert.That(RegexHelper.IsTableContainerNameValid(value), Is.EqualTo(expected));
        }

        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase(" ", false)]
        [TestCase(" 456", false)]
        [TestCase(".456", false)]
        [TestCase("ué456", false)]
        [TestCase("ERT-DDD", false)]
        [TestCase("4Name", false)]
        [TestCase("78", false)] // Min = 3
        [TestCase("abcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabca", false)] // Max = 63
        [TestCase("aname1321", true)]
        [TestCase("ert-ddd", true)]
        public void IsBlobContainerNameValid_Returns_True_When_Name_Is_Valid(string value, bool expected)
        {
            Assert.That(RegexHelper.IsBlobContainerNameValid(value), Is.EqualTo(expected));
        }

        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase(" ", false)]
        [TestCase("abc", false)]
        [TestCase("123", false)]
        [TestCase("b77ad6f9-624a-xxxx-96e8-545923e56502", false)] //[A-F0-9]
        [TestCase("b77ad6f9624a4c2896e8545923e56502", false)] // N format not supported
        [TestCase("00000000-0000-0000-0000-000000000000", true)]
        [TestCase("b77ad6f9-624a-4c28-96e8-545923e56502", true)]
        [TestCase("B77AD6F9-624A-4C28-96E8-545923E56502", true)]
        public void IsGuidTest(string value, bool expected)
        {
            Assert.That(RegexHelper.IsGuid(value), Is.EqualTo(expected));
        }

        [TestCase("63531852249940005_000000000-0000-0000-0000-000000000000", false)]
        [TestCase("635318522499400050_000000000-0000-0000-0000-00000000000", false)]
        [TestCase("635318522499400050_000000000-0000-0000-000-000000000000", false)]
        [TestCase("635318522499400050_00000000000000000000000000000000G", false)]
        [TestCase("0000000000000000000_00000000000000000000000000000000", true)]
        [TestCase("0635318522499400050_b77ad6f9624a4c2896e8545923e56502", true)]
        [TestCase("0635318522499400050_B77AD6F9624A4C2896E8545923E56502", true)]
        [TestCase("0635318522499400050_00000000000000000000000000000000", true)]
        [TestCase("0635318522499400050_00000000000000000000000000000000", true)]
        public void IsSortedGuid(string value, bool expected)
        {
            Assert.That(RegexHelper.IsSortedGuid(value), Is.EqualTo(expected));
        }

        [TestCase("01.1.1", false)]
        [TestCase("1.2", false)]
        [TestCase("1.2.3.DEV", false)]
        [TestCase("1.2-SNAPSHOT", false)]
        [TestCase("1.2.3----RC-SNAPSHOT.12.09.1--..12+788", false)]
        [TestCase("1.2-RC-SNAPSHOT", false)]
        [TestCase("-1.0.3-gamma+b7718", false)]
        [TestCase("1.0.0-alpha-a.b-c-somethinglong+build.1-aef.1-its-okay", true)]
        [TestCase("1.0.0+0.build.1-rc.10000aaa-kk-0.1", true)]
        [TestCase("1.0.0-rc.1+build.1", true)]
        [TestCase("2.0.0-rc.1+build.123", true)]
        [TestCase("0.0.4", true)]
        [TestCase("1.2.3", true)]
        [TestCase("10.20.30", true)]
        [TestCase("1.2.3-beta", true)]
        [TestCase("10.2.3-DEV-SNAPSHOT", true)]
        [TestCase("1.2.3-SNAPSHOT-123", true)]
        [TestCase("1.0.0", true)]
        [TestCase("2.0.0", true)]
        [TestCase("2.0.0+build.1848", true)]
        [TestCase("2.0.1-alpha.1227", true)]
        [TestCase("1.2.3----RC-SNAPSHOT.12.9.1--.12+788", true)]
        public void IsSemver(string value, bool expected)
        {
            Assert.That(RegexHelper.IsSemver(value), Is.EqualTo(expected));
        }
    }
}

namespace SuperMassive.Tests.Unit.Helpers
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using NUnit.Framework;

    public class CryptographyHelperTest
    {
        [TestCaseSource(typeof(TestCases), nameof(TestCases.Md5))]
        public void ComputeMD5Hash_Returns_Expected_Hash(string value, string expected)
        {
#pragma warning disable 618
            Assert.That(CryptographyHelper.ComputeMD5Hash(value), Is.EqualTo(expected));
#pragma warning restore 618
        }

        [TestCaseSource(typeof(TestCases), nameof(TestCases.Md5))]
        public void GetMd5Hash_Returns_Expected_Hash(string value, string expected)
        {
            Assert.That(CryptographyHelper.GetMd5Hash(value), Is.EqualTo(expected));
        }

        [TestCaseSource(typeof(TestCases), nameof(TestCases.Md5))]
        public void ToMd5Hash_Returns_Expected_Hash(string value, string expected)
        {
            Assert.That(value.ToMd5Hash(), Is.EqualTo(expected));
        }

        [TestCaseSource(typeof(TestCases), nameof(TestCases.Md5File))]
        public void ComputeMD5HashFromFile_Returns_Expected_Hash(string value, string expected, bool base64)
        {
#pragma warning disable 618
            AssertHashFromFile(value, expected, base64, CryptographyHelper.ComputeMD5HashFromFile);
#pragma warning restore 618
        }

        [TestCaseSource(typeof(TestCases), nameof(TestCases.Md5File))]
        public void GetMd5HashFromFile_Returns_Expected_Hash(string value, string expected, bool base64)
        {
            AssertHashFromFile(value, expected, base64, CryptographyHelper.GetMd5HashFromFile);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("unknown")]
        public void GetMd5HashFromFile_Returns_Null_When_File_Does_Not_Exist(string filePath)
        {
            Assert.That(CryptographyHelper.GetMd5HashFromFile(filePath), Is.Null);
        }

        [TestCaseSource(typeof(TestCases), nameof(TestCases.Sha1))]
        public void ComputeSHA1Hash_Returns_Expected_Hash(string value, string expected)
        {
#pragma warning disable 618
            Assert.That(CryptographyHelper.ComputeSHA1Hash(value), Is.EqualTo(expected));
#pragma warning restore 618
        }

        [TestCaseSource(typeof(TestCases), nameof(TestCases.Sha1))]
        public void GetSha1Hash_Returns_Expected_Hash(string value, string expected)
        {
            Assert.That(CryptographyHelper.GetSha1Hash(value), Is.EqualTo(expected));
        }

        [TestCaseSource(typeof(TestCases), nameof(TestCases.Sha1))]
        public void ToSha1Hash_Returns_Expected_Hash(string value, string expected)
        {
            Assert.That(value.ToSha1Hash(), Is.EqualTo(expected));
        }

        [TestCaseSource(typeof(TestCases), nameof(TestCases.Sha1File))]
        public void ComputeSHA1HashFromFile_Returns_Expected_Hash(string value, string expected, bool base64 = false)
        {
#pragma warning disable 618
            AssertHashFromFile(value, expected, base64, CryptographyHelper.ComputeSHA1HashFromFile);
#pragma warning restore 618
        }

        [TestCaseSource(typeof(TestCases), nameof(TestCases.Sha1File))]
        public void GetSha1HashFromFile_Returns_Expected_Hash(string value, string expected, bool base64 = false)
        {
            AssertHashFromFile(value, expected, base64, CryptographyHelper.GetSha1HashFromFile);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("unknown")]
        public void GetSha1HashFromFile_Returns_Null_When_File_Does_Not_Exist(string filePath)
        {
            Assert.That(CryptographyHelper.GetSha1HashFromFile(filePath), Is.Null);
        }

        [TestCaseSource(typeof(TestCases), nameof(TestCases.Sha256))]
        public void ComputeSHA256Hash_Returns_Expected_Hash(string value, string expected)
        {
#pragma warning disable 618
            Assert.That(CryptographyHelper.ComputeSHA256Hash(value), Is.EqualTo(expected));
#pragma warning restore 618
        }

        [TestCaseSource(typeof(TestCases), nameof(TestCases.Sha256))]
        public void GetSha256Hash_Returns_Expected_Hash(string value, string expected)
        {
            Assert.That(CryptographyHelper.GetSha256Hash(value), Is.EqualTo(expected));
        }

        [TestCaseSource(typeof(TestCases), nameof(TestCases.Sha256))]
        public void ToSha256Hash_Returns_Expected_Hash(string value, string expected)
        {
            Assert.That(value.ToSha256Hash(), Is.EqualTo(expected));
        }

        [TestCaseSource(typeof(TestCases), nameof(TestCases.Sha256File))]
        public void ComputeSHA256HashFromFile_Returns_Expected_Hash(string value, string expected, bool base64 = false)
        {
#pragma warning disable 618
            AssertHashFromFile(value, expected, base64, CryptographyHelper.ComputeSHA256HashFromFile);
#pragma warning restore 618
        }

        [TestCaseSource(typeof(TestCases), nameof(TestCases.Sha256File))]
        public void GetSha256HashFromFile_Returns_Expected_Hash(string value, string expected, bool base64 = false)
        {
            AssertHashFromFile(value, expected, base64, CryptographyHelper.GetSha256HashFromFile);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("unknown")]
        public void GetSha256HashFromFile_Returns_Null_When_File_Does_Not_Exist(string filePath)
        {
            Assert.That(CryptographyHelper.GetSha256HashFromFile(filePath), Is.Null);
        }

        [TestCaseSource(typeof(TestCases), nameof(TestCases.Crc32))]
        public void ComputeCRC32Hash_Returns_Expected_Hash(string value, string expected)
        {
#pragma warning disable 618
            Assert.That(CryptographyHelper.ComputeCRC32Hash(value), Is.EqualTo(expected));
#pragma warning restore 618
        }

        [TestCaseSource(typeof(TestCases), nameof(TestCases.Crc32))]
        public void GetCrc32Hash_Returns_Expected_Hash(string value, string expected)
        {
            Assert.That(CryptographyHelper.GetCrc32Hash(value), Is.EqualTo(expected));
        }

        [TestCaseSource(typeof(TestCases), nameof(TestCases.Crc32))]
        public void ToCrc32Hash_Returns_Expected_Hash(string value, string expected)
        {
            Assert.That(value.ToCrc32Hash(), Is.EqualTo(expected));
        }

        [TestCaseSource(typeof(TestCases), nameof(TestCases.Crc32File))]
        public void GetCrc32HashFromFile_Returns_Expected_Hash(string value, string expected, bool base64 = false)
        {
            AssertHashFromFile(value, expected, base64, CryptographyHelper.GetCrc32HashFromFile);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("unknown")]
        public void GetCrc32HashFromFile_Returns_Null_When_File_Does_Not_Exist(string filePath)
        {
            Assert.That(CryptographyHelper.GetCrc32HashFromFile(filePath), Is.Null);
        }

        [TestCaseSource(typeof(TestCases), nameof(TestCases.Crc32))]
        public void ComputeCRC32HashByte_Returns_Expected_Hash(string value, string expected)
        {
            Assert.That(
                CryptographyHelper.ByteToHex(
                    CryptographyHelper.ComputeCRC32HashByte(value)),
                Is.EqualTo(expected));
        }

        [TestCaseSource(typeof(TestCases), nameof(TestCases.Crc32))]
        public void GetCrc32HashBytes_Returns_Expected_Hash(string value, string expected)
        {
            Assert.That(
                CryptographyHelper.ByteToHex(
                    CryptographyHelper.GetCrc32HashBytes(value)),
                Is.EqualTo(expected));
        }

        [TestCaseSource(typeof(TestCases), nameof(TestCases.Crc16))]
        public void ComputeCRC16Hash_Returns_Expected_Hash(string value, string expected)
        {
#pragma warning disable 618
            Assert.That(CryptographyHelper.ComputeCRC16Hash(value), Is.EqualTo(expected));
#pragma warning restore 618
        }

        [TestCaseSource(typeof(TestCases), nameof(TestCases.Crc16))]
        public void GetCrc16Hash_Returns_Expected_Hash(string value, string expected)
        {
            Assert.That(CryptographyHelper.GetCrc16Hash(value), Is.EqualTo(expected));
        }

        [TestCaseSource(typeof(TestCases), nameof(TestCases.Crc16))]
        public void ToCrc16Hash_Returns_Expected_Hash(string value, string expected)
        {
            Assert.That(value.ToCrc16Hash(), Is.EqualTo(expected));
        }

        [TestCaseSource(typeof(TestCases), nameof(TestCases.Crc16))]
        public void ComputeCRC16HashByte_Returns_Expected_Hash(string value, string expected)
        {
            Assert.That(
                CryptographyHelper.ByteToHex(
#pragma warning disable 618
                    CryptographyHelper.ComputeCRC16HashByte(value)),
#pragma warning restore 618
                Is.EqualTo(expected));
        }

        [TestCaseSource(typeof(TestCases), nameof(TestCases.Crc16))]
        public void GetCrc16HashBytes_Returns_Expected_Hash(string value, string expected)
        {
            Assert.That(
                CryptographyHelper.ByteToHex(
                    CryptographyHelper.GetCrc16HashBytes(value)),
                Is.EqualTo(expected));
        }

        [TestCaseSource(typeof(TestCases), nameof(TestCases.ByteToHex))]
        public void ByteToHex_Returns_Expected_Hexadecimal_Value(byte[] value, string expected)
        {
            Assert.That(
                CryptographyHelper.ByteToHex(value),
                Is.EqualTo(expected));
        }

        [TestCaseSource(typeof(TestCases), nameof(TestCases.ByteToHex))]
        public void ToHex_Returns_Expected_Hexadecimal_Value(byte[] value, string expected)
        {
            Assert.That(
                value.ToHex(),
                Is.EqualTo(expected));
        }

        [TestCaseSource(typeof(TestCases), nameof(TestCases.Base64))]
        public void EncodeToBase64Test(string value, string expected)
        {
            Assert.That(CryptographyHelper.EncodeToBase64(value), Is.EqualTo(expected));
        }

        [TestCaseSource(typeof(TestCases), nameof(TestCases.Base64))]
        public void DecodeBase64Test(string expected, string value)
        {
            Assert.That(CryptographyHelper.DecodeBase64(value), Is.EqualTo(expected));
        }

        [TestCase("", "d41d8cd98f00b204e9800998ecf8427e")]
        [TestCase(" ", "7215ee9c7d9dc229d2921a40e899ec5f")]
        [TestCase("SuperMassive", "cad0d67d052f61ad42b0010983663a45")]
        [TestCase("SuperMassive", "ytDWfQUvYa1CsAEJg2Y6RQ==", true)]
        public void ComputeHashFromStreamTest(string value, string expected, bool base64 = false)
        {
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(value));
            using var hasher = MD5.Create();

            Assert.That(
                CryptographyHelper.ComputeHashFromStream(stream, hasher, base64),
                Is.EqualTo(expected));
        }

        private static string CreateTemporaryFile(string content)
        {
            var fileName = Path.GetTempFileName();
            File.WriteAllText(fileName, content);
            return fileName;
        }

        private static void AssertHashFromFile(
            string value,
            string expected,
            bool base64,
            Func<string, bool, string> hashMethod)
        {
            var fileName = CreateTemporaryFile(value);
            var actual = hashMethod(fileName, base64);
            if (File.Exists(fileName))
                File.Delete(fileName);

            Assert.That(actual, Is.EqualTo(expected));
        }

        private static class TestCases
        {
            public static readonly object[] Md5 =
            {
                new object[] { null, null },
                new object[] { string.Empty, "d41d8cd98f00b204e9800998ecf8427e" },
                new object[] { " ", "7215ee9c7d9dc229d2921a40e899ec5f" },
                new object[] { "SuperMassive", "cad0d67d052f61ad42b0010983663a45" },
            };

            public static readonly object[] Md5File =
            {
                new object[] { "SuperMassive", "cad0d67d052f61ad42b0010983663a45", false },
                new object[] { "SuperMassive", "ytDWfQUvYa1CsAEJg2Y6RQ==", true },
            };

            public static readonly object[] Sha1 =
            {
                new object[] { null, null },
                new object[] { string.Empty, "da39a3ee5e6b4b0d3255bfef95601890afd80709" },
                new object[] { " ", "b858cb282617fb0956d960215c8e84d1ccf909c6" },
                new object[] { "SuperMassive", "ba3fff6f7041385c8b75901f421e5c2db081ebea" },
            };

            public static readonly object[] Sha1File =
            {
                new object[] { "SuperMassive", "ba3fff6f7041385c8b75901f421e5c2db081ebea", false },
                new object[] { "SuperMassive", "uj//b3BBOFyLdZAfQh5cLbCB6+o=", true },
            };

            public static readonly object[] Sha256 =
            {
                new object[] { null, null },
                new object[] { string.Empty, "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855" },
                new object[] { " ", "36a9e7f1c95b82ffb99743e0c5c4ce95d83c9a430aac59f84ef3cbfab6145068" },
                new object[] { "SuperMassive", "9296c1a118328aad1201f1dcfead4d5aeb84580fae41157afacc4367f3e8b3d9" },
            };

            public static readonly object[] Sha256File =
            {
                new object[]
                    { "SuperMassive", "9296c1a118328aad1201f1dcfead4d5aeb84580fae41157afacc4367f3e8b3d9", false },
                new object[] { "SuperMassive", "kpbBoRgyiq0SAfHc/q1NWuuEWA+uQRV6+sxDZ/Pos9k=", true },
            };

            public static readonly object[] Crc32 =
            {
                new object[] { null, null },
                new object[] { string.Empty, "00000000" },
                new object[] { " ", "e96ccf45" },
                new object[] { "SuperMassive", "93ba819f" },
            };

            public static readonly object[] Crc32File =
            {
                new object[] { "SuperMassive", "93ba819f", false },
                new object[] { "SuperMassive", "k7qBnw==", true },
            };

            public static readonly object[] Crc16 =
            {
                new object[] { null, null },
                new object[] { string.Empty, "0000" },
                new object[] { " ", "0221" },
                new object[] { "SuperMassive", "184b" },
            };

            public static readonly object[] ByteToHex =
            {
                new object[] { null, null },
                new object[] { Encoding.UTF8.GetBytes(string.Empty), string.Empty },
                new object[] { Encoding.UTF8.GetBytes(" "), "20" },
                new object[] { Encoding.UTF8.GetBytes("SuperMassive"), "53757065724d617373697665" },
            };

            public static readonly object[] Base64 =
            {
                new object[] { null, null },
                new object[] { string.Empty, string.Empty },
                new object[] { " ", "IA==" },
                new object[] { "SuperMassive", "U3VwZXJNYXNzaXZl" },
            };
        }
    }
}

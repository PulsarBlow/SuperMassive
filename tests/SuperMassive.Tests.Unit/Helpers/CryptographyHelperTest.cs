namespace SuperMassive.Tests.Unit.Helpers
{
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using NUnit.Framework;

    public class CryptographyHelperTest
    {
        [TestCase("", "")]
        [TestCase(" ", "7215ee9c7d9dc229d2921a40e899ec5f")]
        [TestCase("SuperMassive", "cad0d67d052f61ad42b0010983663a45")]
        public void ComputeMD5Hash_Returns_Expected_Hash(string value, string expected)
        {
            Assert.That(expected, Is.EqualTo(CryptographyHelper.ComputeMD5Hash(value)));
        }

        [TestCase("SuperMassive", "cad0d67d052f61ad42b0010983663a45")]
        [TestCase("SuperMassive", "ytDWfQUvYa1CsAEJg2Y6RQ==", true)]
        public void ComputeMD5HashFromFile_Returns_Expected_Hash(string value, string expected, bool base64 = false)
        {
            var fileName = CreateTemporaryFile(value);

            Assert.That(
                CryptographyHelper.ComputeMD5HashFromFile(fileName, base64),
                Is.EqualTo(expected));

            if(File.Exists(fileName))
                File.Delete(fileName);
        }

        [TestCase("", "")]
        [TestCase(" ", "36a9e7f1c95b82ffb99743e0c5c4ce95d83c9a430aac59f84ef3cbfab6145068")]
        [TestCase("SuperMassive", "9296c1a118328aad1201f1dcfead4d5aeb84580fae41157afacc4367f3e8b3d9")]
        public void ComputeSHA256Hash_Returns_Expected_Hash(string value, string expected)
        {
            Assert.That(CryptographyHelper.ComputeSHA256Hash(value), Is.EqualTo(expected));
        }

        [TestCase("SuperMassive", "9296c1a118328aad1201f1dcfead4d5aeb84580fae41157afacc4367f3e8b3d9")]
        [TestCase("SuperMassive", "kpbBoRgyiq0SAfHc/q1NWuuEWA+uQRV6+sxDZ/Pos9k=", true)]
        public void ComputeSHA256HashFromFile_Returns_Expected_Hash(string value, string expected, bool base64 = false)
        {
            var fileName = CreateTemporaryFile(value);

            Assert.That(
                expected,
                Is.EqualTo(
                    CryptographyHelper.ComputeSHA256HashFromFile(fileName, base64)));

            if (File.Exists(fileName))
                File.Delete(fileName);
        }

        [TestCase("", "")]
        [TestCase(" ", "b858cb282617fb0956d960215c8e84d1ccf909c6")]
        [TestCase("SuperMassive", "ba3fff6f7041385c8b75901f421e5c2db081ebea")]
        public void ComputeSHA1Hash_Returns_Expected_Hash(string value, string expected)
        {
            Assert.That(CryptographyHelper.ComputeSHA1Hash(value), Is.EqualTo(expected));
        }

        [TestCase("SuperMassive", "ba3fff6f7041385c8b75901f421e5c2db081ebea")]
        [TestCase("SuperMassive", "uj//b3BBOFyLdZAfQh5cLbCB6+o=", true)]
        public void ComputeSHA1HashFromFile_Returns_Expected_Hash(string value, string expected, bool base64 = false)
        {
            var fileName = CreateTemporaryFile(value);

            Assert.That(
                CryptographyHelper.ComputeSHA1HashFromFile(fileName, base64),
                Is.EqualTo(expected));

            if (File.Exists(fileName))
                File.Delete(fileName);
        }

        [TestCase("", "")]
        [TestCase(" ", "e96ccf45")]
        [TestCase("SuperMassive", "93ba819f")]
        public void ComputeCRC32Hash_Returns_Expected_Hash(string value, string expected)
        {
            Assert.That(CryptographyHelper.ComputeCRC32Hash(value), Is.EqualTo(expected));
        }

        [Test]
        public void ComputeCRC32HashByte_Returns_Expected_Hash()
        {
            const string input = "2520056747225332399_63a5c229-2461-4e7c-805d-82394a99bd11";
            const string expected = "eff6144a";

            Assert.That(
                CryptographyHelper.ByteToHex(
                    CryptographyHelper.ComputeCRC32HashByte(input)),
                Is.EqualTo(expected));
        }

        [Test]
        public void ComputeCRC16Hash_Returns_Expected_Hash()
        {
            const string input = "2520056747225332399_63a5c229-2461-4e7c-805d-82394a99bd11";
            const string expected = "97a1";

            Assert.That(CryptographyHelper.ComputeCRC16Hash(input), Is.EqualTo(expected));
        }

        [Test]
        public void ComputeCRC16HashByte_Returns_Expected_Bits()
        {
            const string input = "2520056747225332399_63a5c229-2461-4e7c-805d-82394a99bd11";
            const string expected = "97a1";

            Assert.That(
                CryptographyHelper.ByteToHex(
                    CryptographyHelper.ComputeCRC16HashByte(input)),
                Is.EqualTo(expected));
        }

        [TestCase("", "")]
        [TestCase(" ", "20")]
        [TestCase("SuperMassive", "53757065724d617373697665")]
        public void ByteToHex_Returns_Expected_Hexadecimal_Value(string value, string expected)
        {
            Assert.That(
                CryptographyHelper.ByteToHex(Encoding.UTF8.GetBytes(value)),
                Is.EqualTo(expected));
        }

        [TestCase(null)]
        [TestCase(new byte[0])]
        public void ByteToHex_Returns_EmptyString_When_ByteArray_Is_Null_Or_Empty(byte[] value)
        {
            Assert.That(CryptographyHelper.ByteToHex(value), Is.Empty);
        }

        [TestCase("", "")]
        [TestCase(" ", "IA==")]
        [TestCase("SuperMassive", "U3VwZXJNYXNzaXZl")]
        public void EncodeToBase64Test(string value, string expected)
        {
            Assert.That(CryptographyHelper.EncodeToBase64(value), Is.EqualTo(expected));
        }

        [TestCase("", "")]
        [TestCase("IA==", " ")]
        [TestCase("U3VwZXJNYXNzaXZl", "SuperMassive")]
        public void DecodeBase64Test(string value, string expected)
        {
            Assert.That(CryptographyHelper.DecodeBase64(value), Is.EqualTo(expected));
        }

        [TestCase("", "")]
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
    }
}

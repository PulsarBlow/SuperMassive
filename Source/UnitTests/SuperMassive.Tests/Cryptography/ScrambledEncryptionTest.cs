using NUnit.Framework;
using SuperMassive.Cryptography;

namespace SuperMassive.Tests
{
    public class ScrambledEncryptionTest
    {
        string _key = "779DGDO49DKDJS898300DKDKKD93JD";

        [Test]
        public void EncryptTest()
        {
            ScrambledEncryption crypt = new ScrambledEncryption(_key);

            int minLength = 8;
            string value = "123456789",
                   encrypted = crypt.Encrypt(value, minLength),
                   decrypted = crypt.Decrypt(encrypted);

            Assert.AreEqual(value.Length, encrypted.Length);
            Assert.AreEqual(value, decrypted);

            value = "1234";
            encrypted = crypt.Encrypt(value);
            decrypted = crypt.Decrypt(encrypted);
            Assert.AreEqual(minLength, encrypted.Length);
            Assert.AreEqual(value.PadRight(minLength, ' '), decrypted);

        }
    }
}

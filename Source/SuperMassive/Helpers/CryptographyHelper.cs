namespace SuperMassive
{
    using Cryptography;
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Provides cryptographic/hashing helping methods
    /// </summary>
    public static class CryptographyHelper
    {
        /// <summary>
        /// Compute the MD5 hash of a UTF-8 string
        /// </summary>
        /// <param name="input"></param>
        /// <returns>The md5 hashed string</returns>
        public static string ComputeMD5Hash(string input)
        {
            if (input == string.Empty)
                return string.Empty;

            using var hasher = MD5.Create();
            return ByteToHex(hasher.ComputeHash(Encoding.UTF8.GetBytes(input)));
        }

        /// <summary>
        /// Computes the MD5 hash from a file content.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="base64"></param>
        /// <returns></returns>
        public static string ComputeMD5HashFromFile(string fileName, bool base64 = false)
        {
            using var stream = File.OpenRead(fileName);
            using var hasher = MD5.Create();
            return ComputeHashFromStream(stream, hasher, base64);
        }

        /// <summary>
        /// Compute the SHA256 hash of a UTF-8 string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ComputeSHA256Hash(string input)
        {
            if (input == string.Empty)
                return string.Empty;

            using var hasher = SHA256.Create();
            return ByteToHex(hasher.ComputeHash(Encoding.UTF8.GetBytes(input)));
        }

        /// <summary>
        /// Computes the SHA256 hash from a file content.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="base64"></param>
        /// <returns></returns>
        public static string ComputeSHA256HashFromFile(string fileName, bool base64 = false)
        {
            using var stream = File.OpenRead(fileName);
            using var hasher = SHA256.Create();
            return ComputeHashFromStream(stream, hasher, base64);
        }

        /// <summary>
        /// Compute the SHA1 hash of a UTF-8 string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ComputeSHA1Hash(string input)
        {
            using var hasher = SHA1.Create();
            return ByteToHex(hasher.ComputeHash(Encoding.UTF8.GetBytes(input)));
        }

        /// <summary>
        /// Computes the SHA1 hash from a file content.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="base64"></param>
        /// <returns></returns>
        public static string ComputeSHA1HashFromFile(string fileName, bool base64 = false)
        {
            using var stream = File.OpenRead(fileName);
            using var hasher = SHA1.Create();
            return ComputeHashFromStream(stream, hasher, base64);
        }

        /// <summary>
        /// Returns the Hexadecimal representation of the byte array
        /// </summary>
        /// <param name="bits"></param>
        /// <returns></returns>
        public static string ByteToHex(byte[] bits)
        {
            if (bits == null)
                return string.Empty;

            var hex = new StringBuilder(bits.Length * 2);
            foreach (var item in bits)
            {
                hex.Append(item.ToString("x2"));
            }

            return hex.ToString();
        }

        /// <summary>
        /// This method encodes an UTF-8 string into a Base64 string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string EncodeToBase64(string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// This method decodes a Base64 string into an UTF-8 string
        /// </summary>
        /// <param name="encodedValue"></param>
        /// <returns></returns>
        public static string DecodeBase64(string encodedValue)
        {
            var bytes = Convert.FromBase64String(encodedValue);
            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// This method computes the CRC32 hash of the given value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ComputeCRC32Hash(string value)
        {
            var crc32 = new Crc32();
            return ByteToHex(crc32.ComputeHash(Encoding.UTF8.GetBytes(value)));
        }

        /// <summary>
        /// This method computes the CRC32 hash of the given value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] ComputeCRC32HashByte(string value)
        {
            var crc32 = new Crc32();
            return crc32.ComputeHash(Encoding.UTF8.GetBytes(value));
        }

        /// <summary>
        /// This method compute the CRC16 hash of the given value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ComputeCRC16Hash(string value)
        {
            Guard.ArgumentNotNullOrWhiteSpace(value, nameof(value));

            var crc16 = new Crc16Ccitt();
            return ByteToHex(crc16.ComputeHashBytes(Encoding.UTF8.GetBytes(value)));
        }

        /// <summary>
        /// This method computes the CRC16 hash of the given value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] ComputeCRC16HashByte(string value)
        {
            var crc16 = new Crc16Ccitt();
            return crc16.ComputeHashBytes(Encoding.UTF8.GetBytes(value));
        }

        internal static string ComputeHashFromStream(Stream stream, HashAlgorithm hasher, bool base64)
        {
            if (stream.Length == 0)
                return string.Empty;

            return base64
                ? Convert.ToBase64String(hasher.ComputeHash(stream))
                : BitConverter.ToString(hasher.ComputeHash(stream)).Replace("-", string.Empty);
        }
    }
}

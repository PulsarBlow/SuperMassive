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
        /// Compute the MD5 hash of a UTF-8 string and returns its hexadecimal string representation
        /// </summary>
        /// <param name="value">Value to hash</param>
        /// <returns>The hexadecimal representation of the MD5 hash or null if the provided value is null</returns>
        [Obsolete("Deprecated. Use GetMd5Hash() instead")]
        public static string? ComputeMD5Hash(string value)
            => GetMd5Hash(value);

        /// <summary>
        /// Compute the MD5 hash of a UTF-8 string and returns its hexadecimal string representation
        /// </summary>
        /// <param name="value">Value to hash</param>
        /// <returns>The hexadecimal representation of the MD5 hash or null if the provided value is null</returns>
        public static string? GetMd5Hash(string value)
        {
            if (value == null)
            {
                return null;
            }

            using var hasher = MD5.Create();
            return ByteToHex(hasher.ComputeHash(Encoding.UTF8.GetBytes(value)));
        }

        /// <summary>
        /// Compute the MD5 hash of a UTF-8 string and returns its hexadecimal string representation
        /// </summary>
        /// <param name="value">Value to hash</param>
        /// <returns>The hexadecimal representation of the MD5 hash or null if the provided value is null</returns>
        public static string? ToMd5Hash(this string value)
            => GetMd5Hash(value);

        /// <summary>
        /// Computes the MD5 hash of a file content and returns its hexadecimal string representation
        /// </summary>
        /// <param name="filePath">Path to the file</param>
        /// <param name="base64">True if the hexadecimal representation should be encoded into base64 prior to be returned</param>
        /// <returns>The hexadecimal representation of the MD5 hash or null if the file does not exist</returns>
        [Obsolete("Deprecated. Use GetMd5HashFromFile() instead")]
        public static string? ComputeMD5HashFromFile(string filePath, bool base64 = false)
            => GetMd5HashFromFile(filePath, base64);

        /// <summary>
        /// Computes the MD5 hash of a file content and returns its hexadecimal string representation
        /// </summary>
        /// <param name="filePath">Path to the file</param>
        /// <param name="base64">True if the hexadecimal representation should be encoded into base64 prior to be returned</param>
        /// <returns>The hexadecimal representation of the MD5 hash or null if the file does not exist</returns>
        public static string? GetMd5HashFromFile(string filePath, bool base64 = false)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }

            using var stream = File.OpenRead(filePath);
            using var hasher = MD5.Create();
            return ComputeHashFromStream(stream, hasher, base64);
        }

        /// <summary>
        /// Compute the SHA-1 hash of a UTF-8 string and returns its hexadecimal string representation
        /// </summary>
        /// <param name="value">Value to hash</param>
        /// <returns>The hexadecimal representation of the SHA-1 hash or null if the provided value is null</returns>
        [Obsolete("Deprecated. Use GetSha1Hash() instead")]
        public static string? ComputeSHA1Hash(string value)
            => GetSha1Hash(value);

        /// <summary>
        /// Compute the SHA-1 hash of a UTF-8 string and returns its hexadecimal string representation
        /// </summary>
        /// <param name="value">Value to hash</param>
        /// <returns>The hexadecimal representation of the SHA-1 hash or null if the provided value is null</returns>
        public static string? GetSha1Hash(string value)
        {
            if (value == null)
            {
                return null;
            }

            using var hasher = SHA1.Create();
            return ByteToHex(hasher.ComputeHash(Encoding.UTF8.GetBytes(value)));
        }

        /// <summary>
        /// Compute the SHA-1 hash of a UTF-8 string and returns its hexadecimal string representation
        /// </summary>
        /// <param name="value">Value to hash</param>
        /// <returns>The hexadecimal representation of the SHA-1 hash or null if the provided value is null</returns>
        public static string? ToSha1Hash(this string value)
            => GetSha1Hash(value);

        /// <summary>
        /// Computes the SHA-1 hash of a file content and returns its hexadecimal string representation
        /// </summary>
        /// <param name="filePath">Path to the file</param>
        /// <param name="base64">True if the hexadecimal representation should be encoded into base64 prior to be returned</param>
        /// <returns>The hexadecimal representation of the SHA-1 hash or null if the file does not exist</returns>
        [Obsolete("Deprecated. Use GetSha1HashFromFile() instead")]
        public static string? ComputeSHA1HashFromFile(string filePath, bool base64 = false)
            => GetSha1HashFromFile(filePath, base64);

        /// <summary>
        /// Computes the SHA-1 hash of a file content and returns its hexadecimal string representation
        /// </summary>
        /// <param name="filePath">Path to the file</param>
        /// <param name="base64">True if the hexadecimal representation should be encoded into base64 prior to be returned</param>
        /// <returns>The hexadecimal representation of the SHA-1 hash or null if the file does not exist</returns>
        public static string? GetSha1HashFromFile(string filePath, bool base64 = false)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }

            using var stream = File.OpenRead(filePath);
            using var hasher = SHA1.Create();
            return ComputeHashFromStream(stream, hasher, base64);
        }

        /// <summary>
        /// Compute the SHA-256 hash of a UTF-8 string and returns its hexadecimal string representation
        /// </summary>
        /// <param name="value">Value to hash</param>
        /// <returns>The hexadecimal representation of the SHA-256 hash or null if the provided value is null</returns>
        [Obsolete("Deprecated. Use GetSha256Hash() instead")]
        public static string? ComputeSHA256Hash(string value)
            => GetSha256Hash(value);

        /// <summary>
        /// Compute the SHA-256 hash of a UTF-8 string and returns its hexadecimal string representation
        /// </summary>
        /// <param name="value">Value to hash</param>
        /// <returns>The hexadecimal representation of the SHA-256 hash or null if the provided value is null</returns>
        public static string? GetSha256Hash(string value)
        {
            if (value == null)
            {
                return null;
            }

            using var hasher = SHA256.Create();
            return ByteToHex(hasher.ComputeHash(Encoding.UTF8.GetBytes(value)));
        }

        /// <summary>
        /// Compute the SHA-256 hash of a UTF-8 string and returns its hexadecimal string representation
        /// </summary>
        /// <param name="value">Value to hash</param>
        /// <returns>The hexadecimal representation of the SHA-256 hash or null if the provided value is null</returns>
        public static string? ToSha256Hash(this string value)
            => GetSha256Hash(value);

        /// <summary>
        /// Computes the SHA-256 hash of a file content and returns its hexadecimal string representation
        /// </summary>
        /// <param name="filePath">Path to the file</param>
        /// <param name="base64">True if the hexadecimal representation should be encoded into base64 prior to be returned</param>
        /// <returns>The hexadecimal representation of the SHA-256 hash or null if the file does not exist</returns>
        [Obsolete("Deprecated. Use GetSha256HashFromFile() instead")]
        public static string? ComputeSHA256HashFromFile(string filePath, bool base64 = false)
            => GetSha256HashFromFile(filePath, base64);

        /// <summary>
        /// Computes the SHA-256 hash of a file content and returns its hexadecimal string representation
        /// </summary>
        /// <param name="filePath">Path to the file</param>
        /// <param name="base64">True if the hexadecimal representation should be encoded into base64 prior to be returned</param>
        /// <returns>The hexadecimal representation of the SHA-256 hash or null if the file does not exist</returns>
        public static string? GetSha256HashFromFile(string filePath, bool base64 = false)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }

            using var stream = File.OpenRead(filePath);
            using var hasher = SHA256.Create();
            return ComputeHashFromStream(stream, hasher, base64);
        }


        /// <summary>
        /// Compute the CRC-32 hash of a UTF-8 string and returns its hexadecimal string representation
        /// </summary>
        /// <param name="value">Value to hash</param>
        /// <returns>The hexadecimal representation of the CRC-32 hash or null if the provided value is null</returns>
        [Obsolete("Deprecated. Use GetCrc32Hash() instead")]
        public static string? ComputeCRC32Hash(string value)
            => GetCrc32Hash(value);

        /// <summary>
        /// Compute the CRC-32 hash of a UTF-8 string and returns its hexadecimal string representation
        /// </summary>
        /// <param name="value">Value to hash</param>
        /// <returns>The hexadecimal representation of the CRC-32 hash or null if the provided value is null</returns>
        public static string? GetCrc32Hash(string value)
        {
            if (value == null)
            {
                return null;
            }

            using var crc32 = new Crc32();
            return ByteToHex(crc32.ComputeHash(Encoding.UTF8.GetBytes(value)));
        }

        /// <summary>
        /// Compute the CRC-32 hash of a UTF-8 string and returns its hexadecimal string representation
        /// </summary>
        /// <param name="value">Value to hash</param>
        /// <returns>The hexadecimal representation of the CRC-32 hash or null if the provided value is null</returns>
        public static string? ToCrc32Hash(this string value)
            => GetCrc32Hash(value);

        /// <summary>
        /// Computes the CRC-32 hash of a file content and returns its hexadecimal string representation
        /// </summary>
        /// <param name="filePath">Path to the file</param>
        /// <param name="base64">True if the hexadecimal representation should be encoded into base64 prior to be returned</param>
        /// <returns>The hexadecimal representation of the CRC-32 hash or null if the file does not exist</returns>
        public static string? GetCrc32HashFromFile(string filePath, bool base64 = false)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }

            using var stream = File.OpenRead(filePath);
            using var hasher = new Crc32();
            return ComputeHashFromStream(stream, hasher, base64);
        }

        /// <summary>
        /// Compute the CRC-32 hash of a UTF-8 string
        /// </summary>
        /// <param name="value">Value to hash</param>
        /// <returns>An array of bytes or null if the provided value is null</returns>
        public static byte[]? ComputeCRC32HashByte(string value)
            => GetCrc32HashBytes(value);

        /// <summary>
        /// Compute the CRC-32 hash of a UTF-8 string
        /// </summary>
        /// <param name="value">Value to hash</param>
        /// <returns>An array of bytes or null if the provided value is null</returns>
        public static byte[]? GetCrc32HashBytes(string value)
        {
            if (value == null)
            {
                return null;
            }

            using var crc32 = new Crc32();
            return crc32.ComputeHash(Encoding.UTF8.GetBytes(value));
        }

        /// <summary>
        /// Compute the CRC-16 hash of a UTF-8 string and returns its hexadecimal string representation
        /// </summary>
        /// <param name="value">Value to hash</param>
        /// <returns>The hexadecimal representation of the CRC-16 hash or null if the provided value is null</returns>
        [Obsolete("Deprecated. Use GetCrc16() instead")]
        public static string? ComputeCRC16Hash(string value)
            => GetCrc16Hash(value);

        /// <summary>
        /// Compute the CRC-16 hash of a UTF-8 string and returns its hexadecimal string representation
        /// </summary>
        /// <param name="value">Value to hash</param>
        /// <returns>The hexadecimal representation of the CRC-16 hash or null if the provided value is null</returns>
        public static string? GetCrc16Hash(string value)
        {
            if (value == null)
            {
                return null;
            }

            var crc16 = new Crc16Ccitt();
            return ByteToHex(crc16.ComputeHashBytes(Encoding.UTF8.GetBytes(value)));
        }

        /// <summary>
        /// Compute the CRC-16 hash of a UTF-8 string and returns its hexadecimal string representation
        /// </summary>
        /// <param name="value">Value to hash</param>
        /// <returns>The hexadecimal representation of the CRC-16 hash or null if the provided value is null</returns>
        public static string? ToCrc16Hash(this string value)
            => GetCrc16Hash(value);

        /// <summary>
        /// Compute the CRC-16 hash of a UTF-8 string
        /// </summary>
        /// <param name="value">Value to hash</param>
        /// <returns>An array of bytes or null if the provided value is null</returns>
        [Obsolete("Deprecated. Use GetCrc16HashBytes() instead")]
        public static byte[]? ComputeCRC16HashByte(string value)
            => GetCrc16HashBytes(value);

        /// <summary>
        /// Compute the CRC-16 hash of a UTF-8 string
        /// </summary>
        /// <param name="value">Value to hash</param>
        /// <returns>An array of bytes or null if the provided value is null</returns>
        public static byte[]? GetCrc16HashBytes(string value)
        {
            if (value == null)
            {
                return null;
            }

            var crc16 = new Crc16Ccitt();
            return crc16.ComputeHashBytes(Encoding.UTF8.GetBytes(value));
        }

        /// <summary>
        /// Returns the Hexadecimal representation of the byte array
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The hexadecimal string representation or null if the provided value is null</returns>
        public static string? ByteToHex(byte[] value)
        {
            if (value == null)
            {
                return null;
            }

            var hex = new StringBuilder(value.Length * 2);
            foreach (var item in value)
            {
                hex.Append(item.ToString("x2"));
            }

            return hex.ToString();
        }

        /// <summary>
        /// Returns the Hexadecimal representation of the byte array
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The hexadecimal string representation or null if the provided value is null</returns>
        public static string? ToHex(this byte[] value)
            => ByteToHex(value);

        /// <summary>
        /// Encodes a string into its base64 representation
        /// </summary>
        /// <param name="value">The value to encode</param>
        /// <returns>The base64 representation or null if the provided value is null</returns>
        public static string? EncodeToBase64(string value)
        {
            return value == null ? null : Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
        }

        /// <summary>
        /// Decodes a base64 string
        /// </summary>
        /// <param name="value">The value to decode</param>
        /// <returns>The decoded string or null if the provided value is null</returns>
        public static string? DecodeBase64(string value)
        {
            return value == null ? null : Encoding.UTF8.GetString(Convert.FromBase64String(value));
        }

        // Internal for testing purposes
        internal static string? ComputeHashFromStream(Stream stream, HashAlgorithm hasher, bool base64)
        {
            return base64
                ? Convert.ToBase64String(hasher.ComputeHash(stream))
                : ByteToHex(hasher.ComputeHash(stream));
        }
    }
}

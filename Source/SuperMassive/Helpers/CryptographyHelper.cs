using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SuperMassive
{
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
            if (String.IsNullOrEmpty(input))
                return null;
            MD5 md5Hasher = null;
            byte[] data = null;
            try
            {
                md5Hasher = MD5.Create();
                data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(input));
            }
            finally
            {
                if (md5Hasher != null)
                {
                    md5Hasher.Dispose();
                    md5Hasher = null;
                }
            }
            return ByteToHex(data);
        }
        /// <summary>
        /// Computes the MD5 hash from a file content.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string ComputeMD5HashFromFile(string fileName)
        {
            return ComputeMD5HashFromFile(fileName, false);
        }
        /// <summary>
        /// Computes the MD5 hash from a file content.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="base64"></param>
        /// <returns></returns>
        public static string ComputeMD5HashFromFile(string fileName, bool base64)
        {
            FileStream stream = null;
            MD5 hasher = null;

            try
            {
                stream = File.OpenRead(fileName);
                hasher = MD5.Create();
                if (base64)
                    return Convert.ToBase64String(hasher.ComputeHash(stream));
                else
                    return BitConverter.ToString(hasher.ComputeHash(stream)).Replace("-", "");
            }
            finally
            {
                # region Freeing Resources

                if (hasher != null)
                {
                    hasher.Dispose();
                    hasher = null;
                }
                if (stream != null)
                {
                    stream.Dispose();
                    stream = null;
                }

                # endregion
            }
        }
        /// <summary>
        /// Compute the SHA256 hash of a UTF-8 string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ComputeSHA256Hash(string input)
        {
            if (String.IsNullOrEmpty(input))
                return null;
            SHA256 sha = null;
            byte[] data = null;
            try
            {
                sha = SHA256.Create();
                data = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
            }
            finally
            {
                if (sha != null)
                {
                    sha.Dispose();
                    sha = null;
                }

            }
            return ByteToHex(data);
        }
        /// <summary>
        /// Computes the SHA256 hash from a file content.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string ComputeSHA256HashFromFile(string fileName)
        {
            return ComputeSHA256HashFromFile(fileName, false);
        }
        /// <summary>
        /// Computes the SHA256 hash from a file content.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="base64"></param>
        /// <returns></returns>
        public static string ComputeSHA256HashFromFile(string fileName, bool base64)
        {
            FileStream stream = null;
            SHA256 hasher = null;

            try
            {
                stream = File.OpenRead(fileName);
                hasher = SHA256.Create();
                if (base64)
                    return Convert.ToBase64String(hasher.ComputeHash(stream));
                else
                    return BitConverter.ToString(hasher.ComputeHash(stream)).Replace("-", "");
            }
            finally
            {
                # region Freeing Resources

                if (hasher != null)
                {
                    hasher.Dispose();
                    hasher = null;
                }
                if (stream != null)
                {
                    stream.Dispose();
                    stream = null;
                }

                # endregion
            }
        }
        /// <summary>
        /// Compute the SHA1 hash of a UTF-8 string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ComputeSHA1Hash(string input)
        {
            if (String.IsNullOrEmpty(input))
                return null;
            SHA1 sha = null;
            byte[] data = null;
            try
            {
                sha = SHA1.Create();
                data = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
            }
            finally
            {
                if (sha != null)
                {
                    sha.Dispose();
                    sha = null;
                }
            }
            return ByteToHex(data);
        }
        /// <summary>
        /// Computes the SHA1 hash from a file content.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string ComputeSHA1HashFromFile(string fileName)
        {
            return ComputeSHA1HashFromFile(fileName, false);
        }
        /// <summary>
        /// Computes the SHA1 hash from a file content.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="base64"></param>
        /// <returns></returns>
        public static string ComputeSHA1HashFromFile(string fileName, bool base64)
        {
            FileStream stream = null;
            SHA1 hasher = null;

            try
            {
                stream = File.OpenRead(fileName);
                hasher = SHA1.Create();
                if (base64)
                    return Convert.ToBase64String(hasher.ComputeHash(stream));
                else
                    return BitConverter.ToString(hasher.ComputeHash(stream)).Replace("-", "");
            }
            finally
            {
                # region Freeing Resources

                if (hasher != null)
                {
                    hasher.Dispose();
                    hasher = null;
                }
                if (stream != null)
                {
                    stream.Dispose();
                    stream = null;
                }

                # endregion
            }
        }
        /// <summary>
        /// Returns the Hexadecimal representation of the byte array
        /// </summary>
        /// <param name="bits"></param>
        /// <returns></returns>
        public static string ByteToHex(byte[] bits)
        {
            if (bits == null)
                return null;
            StringBuilder hex = new StringBuilder(bits.Length * 2);
            for (int i = 0; i < bits.Length; i++)
            {
                hex.Append(bits[i].ToString("x2"));
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
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(value);
            return System.Convert.ToBase64String(bytes);
        }
        /// <summary>
        /// This method decodes a Base64 string into an UTF-8 string
        /// </summary>
        /// <param name="encodedValue"></param>
        /// <returns></returns>
        public static string DecodeBase64(string encodedValue)
        {
            byte[] bytes = System.Convert.FromBase64String(encodedValue);
            return System.Text.Encoding.UTF8.GetString(bytes);
        }
    }
}

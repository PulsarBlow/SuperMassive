#nullable enable

namespace SuperMassive
{
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    /// <summary>
    /// Provides helping methods to handle binary serialization
    /// </summary>
    public static class BinarySerializationHelper
    {
        /// <summary>
        /// Serialize an object into a byte array
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] Serialize(object value)
        {
            var bf = new BinaryFormatter();

            using var memoryStream = new MemoryStream();
            bf.Serialize(memoryStream, value);
            memoryStream.Flush();

            return memoryStream.GetBuffer();
        }

        /// <summary>
        /// Deserialize a byte array into an object
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object Deserialize(byte[] value)
        {
            var bf = new BinaryFormatter();
            using var memoryStream = new MemoryStream(value);
            return bf.Deserialize(memoryStream);
        }
    }
}

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SuperMassive
{
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
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Serialize the object in memory
                bf.Serialize(memoryStream, value);

                // Make sure all is loaded in the stream
                memoryStream.Flush();

                return memoryStream.GetBuffer();
            }
        }
        /// <summary>
        /// Deserialize a byte array into an object
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object Deserialize(byte[] value)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream(value))
            {
                return bf.Deserialize(memoryStream);
            }
        }
    }
}

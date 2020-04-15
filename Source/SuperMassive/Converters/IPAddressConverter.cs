#nullable enable

namespace SuperMassive
{
    using System;
    using System.Linq;

    /// <summary>
    /// Provides utility methods to manipulate ipaddress
    /// </summary>
    public static class IpAddressConverter
    {
        /// <summary>
        /// Converts a dot notated ip address into its <see cref="Int64"/> counterpart.
        /// Takes care of LittleEndian / Big Endian order
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static long ToInt64(string address)
        {
            byte[] ip = address
                .Split('.')
                .Select(byte.Parse)
                .ToArray();

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(ip);
            }

            return BitConverter.ToUInt32(ip, 0);
        }
    }
}

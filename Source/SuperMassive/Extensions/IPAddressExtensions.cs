using System.Net;

namespace SuperMassive
{
    /// <summary>
    /// Provides extensions methods for the <see cref="IPAddress"/> class.
    /// </summary>
    public static class IPAddressExtensions
    {
        /// <summary>
        /// Returns the long value if this ip address
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static long ToLongValue(this IPAddress address)
        {
            return IPAddressConverter.ToInt64(address.ToString());
        }
    }
}

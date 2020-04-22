namespace SuperMassive.Extensions
{
    using System.Net;

    /// <summary>
    /// Provides extensions methods for the <see cref="IPAddress"/> class.
    /// </summary>
    public static class IpAddressExtensions
    {
        /// <summary>
        /// Returns the long value if this ip address
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static long ToLongValue(this IPAddress address)
        {
            return IpAddressConverter.ToInt64(address.ToString());
        }
    }
}

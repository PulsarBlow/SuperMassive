namespace SuperMassive
{
    using System;

    /// <summary>
    /// Provides helping method for manipulating dates. Provides DateTime extension methods too
    /// </summary>
    public static class DateHelper
    {
        /// <summary>
        /// Ensures that local times are converted to UTC times.  Unspecified kinds are recast to UTC with no conversion.
        /// </summary>
        /// <param name="value">The date-time to convert.</param>
        /// <returns>The date-time in UTC time.</returns>
        public static DateTime AsUtc(this DateTime value)
        {
            return value.Kind == DateTimeKind.Unspecified ?
                new DateTime(value.Ticks, DateTimeKind.Utc) :
                value.ToUniversalTime();
        }

        /// <summary>
        /// Ensures that local times are converted to UTC times.  Unspecified kinds are recast to UTC with no conversion.
        /// </summary>
        /// <param name="value">The nullable date-time to convert.</param>
        /// <returns>The nullable date-time in UTC time.</returns>
        public static DateTime? AsUtc(this DateTime? value)
        {
            if (!value.HasValue) { return null; }

            return value.Value.Kind == DateTimeKind.Unspecified ?
                new DateTime(value.Value.Ticks, DateTimeKind.Utc) :
                value.Value.ToUniversalTime();
        }

        /// <summary>
        /// Convert a unix timestamp to the corresponding datetime.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime FromUnixTime(double value)
        {
            var unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return unixStart.AddSeconds(value);
        }
        /// <summary>
        /// Convert a datetime to its corresponding unix timestamp value.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static long ToUnixTime(DateTime date)
        {
            var unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            var timespan = date - unixStart;
            return (long)Math.Floor(timespan.TotalSeconds);
        }
    }
}

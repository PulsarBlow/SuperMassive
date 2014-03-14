using System;

namespace SuperMassive
{
    /// <summary>
    /// DateTime Extension methods
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Trims a DateTime object to a specific precision
        /// </summary>
        /// <param name="date"></param>
        /// <param name="roundTicks"></param>
        /// <returns></returns>
        /// <code>
        /// DateTime.Now.Trim(TimeSpan.TicksPerDay);
        /// DateTime.Now.Trim(TimeSpan.TicksPerHour);
        /// DateTime.Now.Trim(TimeSpan.TicksPerMinute);
        /// DateTime.Now.Trim(TimeSpan.TicksPerSecond); => compares to second precision
        /// DateTime.Now.Trim(TimeSpan.TicksPerMillisecond);
        /// </code>
        public static DateTime Trim(this DateTime date, long roundTicks)
        {
            return new DateTime(date.Ticks - date.Ticks % roundTicks, date.Kind);
        }
        /// <summary>
        /// Round a date up to midpoint.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="span"></param>
        /// <returns></returns>
        /// <example>DateTime nearestHour = DateTime.Now.Round(new TimeSpan(1,0,0));</example>
        public static DateTime Round(this DateTime date, TimeSpan span)
        {
            long midpoint = ((span.Ticks + 1) >> 1);
            return date.AddTicks(midpoint - ((date.Ticks + midpoint) % span.Ticks));
        }
        /// <summary>
        /// Floor a date
        /// </summary>
        /// <param name="date"></param>
        /// <param name="span"></param>
        /// <returns></returns>
        /// <example>DateTime weekFloor = DateTime.Now.Floor(new TimeSpan(7,0,0,0));</example>
        public static DateTime Floor(this DateTime date, TimeSpan span)
        {
            return date.AddTicks(-(date.Ticks % span.Ticks));
        }
        /// <summary>
        /// Ceil a date
        /// </summary>
        /// <param name="date"></param>
        /// <param name="span"></param>
        /// <returns></returns>
        /// <example>DateTime minuteCeiling = DateTime.Now.Ceil(new TimeSpan(0,1,0));</example>
        public static DateTime Ceil(this DateTime date, TimeSpan span)
        {
            return date.AddTicks(span.Ticks - (date.Ticks % span.Ticks));
        }
        /// <summary>
        /// Get UnixTimeStamp
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ToUnixTime(this DateTime value)
        {
            DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, value.Kind);
            TimeSpan timespan = value - unixStart;
            return timespan.TotalSeconds;
        }
    }
}

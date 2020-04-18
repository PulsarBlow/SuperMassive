﻿namespace SuperMassive.Extensions
{
    using System;

    /// <summary>
    /// Provides extension methods for the <see cref="Random"/> class
    /// </summary>
    public static class RandomExtensions
    {
        /// <summary>
        /// Returns a random integer value across the entire range of possible values (negative ones included).
        /// </summary>
        /// <param name="extended"></param>
        /// <returns></returns>
        public static int NextInt32(this Random extended)
        {
            int firstBits = extended.Next(0, 1 << 4) << 28;
            int lastBits = extended.Next(0, 1 << 28);
            return firstBits | lastBits;
        }

        /// <summary>
        /// Returns a random decimal. Distribution is not uniform.
        /// </summary>
        /// <param name="extended"></param>
        /// <param name="alwaysPositive">True is the returned decimal should always be positive</param>
        /// <returns></returns>
        public static decimal NextDecimal(this Random extended, bool alwaysPositive = false)
        {
            byte scale = (byte)extended.Next(29);
            bool isNegative = !alwaysPositive && extended.Next(2) == 1;
            return new Decimal(extended.NextInt32(), extended.NextInt32(), extended.NextInt32(), isNegative, scale);
        }
    }
}

namespace SuperMassive.Extensions
{
    using System;

    /// <summary>
    /// Provides extension methods for the <see cref="Random"/> class
    /// </summary>
    public static class RandomExtensions
    {
        /// <summary>
        /// Returns a random integer value accross the entire range of possible values.
        /// </summary>
        /// <param name="rng"></param>
        /// <returns></returns>
        public static int NextInt32(this Random rng)
        {
            unchecked
            {
                int firstBits = rng.Next(0, 1 << 4) << 28;
                int lastBits = rng.Next(0, 1 << 28);
                return firstBits | lastBits;
            }
        }

        /// <summary>
        /// Returns a random decimal. Distribution is not uniform.
        /// </summary>
        /// <remarks> http://stackoverflow.com/a/609529 </remarks>
        /// <param name="rng"></param>
        /// <param name="alwaysPositive">True is the returned decimal should always be positive</param>
        /// <returns></returns>
        public static decimal NextDecimal(this Random rng, bool alwaysPositive = false)
        {
            byte scale = (byte)rng.Next(29);
            bool isNegative = alwaysPositive ? false : rng.Next(2) == 1;
            return new Decimal(rng.NextInt32(), rng.NextInt32(), rng.NextInt32(), isNegative, scale);
        }
    }
}

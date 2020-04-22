namespace SuperMassive
{
    using System;
    using System.Globalization;
    using System.Security.Cryptography;
    using System.Text;
    using Properties;

    /// <summary>
    /// Provides utilities to generate random primitives.
    /// </summary>
    public static class Randomizer
    {
        private const string RANDOM_POSSIBLE_CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private static readonly char[] _randomChars = RANDOM_POSSIBLE_CHARS.ToCharArray();
        private static readonly Random _random = new Random();

        /// <summary>
        /// Gets a random int between 0 and max (half-inclusive)
        /// </summary>
        /// <param name="max">Maximum excluded value</param>
        /// <returns>A random generated integer</returns>
        public static int GetInt(int max = int.MaxValue)
        {
            return _random.Next(max);
        }

        /// <summary>
        /// Gets a random int between min and max (half-inclusive)
        /// </summary>
        /// <param name="min">Minimum included value</param>
        /// <param name="max">Maximum excluded value</param>
        /// <returns>A random generated integer</returns>
        public static int GetInt(int min, int max)
        {
            return GetInt(max - min) + min;
        }

        /// <summary>
        /// Gets a random float between 0 and 1 (full inclusive)
        /// </summary>
        /// <returns>A random generated float</returns>
        public static float GetFloat()
        {
            return GetFloat(1);
        }

        /// <summary>
        /// Gets a random float between 0 and max (full inclusive)
        /// </summary>
        /// <param name="max">Maximum included value</param>
        /// <returns>A random generated float</returns>
        public static float GetFloat(float max)
        {
            if (max < 0.0f) throw new ArgumentOutOfRangeException("max", Resources.GENERIC_GUARD_FAILURE_VALUESTRICTLYPOSITIVE);

            return (float)_random.NextDouble() * max;
        }

        /// <summary>
        /// Gets a random float between min and max.
        /// </summary>
        /// <param name="min">Minimum included value</param>
        /// <param name="max">Maximum included value</param>
        /// <returns>A random generated float</returns>
        public static float GetFloat(float min, float max)
        {
            if (max < min)
            {
                throw new ArgumentOutOfRangeException(
                nameof(max),
                string.Format(
                    CultureInfo.CurrentCulture,
                    Resources.GENERIC_GUARD_FAILURE_ARGUMENT_MUSTBE_GREATERTHAN_WITHFORMAT,
                    min,
                    max));
            }

            return GetFloat(max - min) + min;
        }

        /// <summary>
        /// Generates a random string suitable for security related uses.
        /// </summary>
        /// <param name="length">The length of the generated string</param>
        /// <returns>A random secured string</returns>
        /// <exception cref="ArgumentException">Argument length is null or negative</exception>
        public static string GetSecureRandomString(int? length = 64)
        {
            if (!length.HasValue || length.Value <= 0)
            {
                throw new ArgumentException(Resources.GENERIC_GUARD_FAILURE_VALUEPOSITIVE, "length");
            }

            byte[] buffer = new byte[length.Value];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetNonZeroBytes(buffer);
            }

            StringBuilder result = new StringBuilder(length.Value);
            foreach (byte b in buffer)
            {
                result.Append(_randomChars[b % _randomChars.Length]);
            }

            return result.ToString();
        }

        /// <summary>
        /// Generates a fast random string, unsuitable for secure uses.
        /// </summary>
        /// <param name="length">The length of the generated string</param>
        /// <returns>Returns a random string</returns>
        /// <exception cref="ArgumentException">Argument length is null or negative</exception>
        public static string GetRandomString(int? length = 64)
        {
            if (!length.HasValue || length.Value <= 0)
            {
                throw new ArgumentException(Resources.GENERIC_GUARD_FAILURE_VALUEPOSITIVE, "length");
            }

            char[] result = new char[length.Value];
            for (int i = 0; i < length.Value; i++)
            {
                result[i] = _randomChars[_random.Next(_randomChars.Length)];
            }

            return new string(result);
        }

        /// <summary>
        /// Generates one Bernoulli Trial.
        /// </summary>
        /// <param name="rnd">The random value to use.</param>
        /// <param name="p">The probability of generating a one (between 0 and 1).</param>
        /// <returns>A random sample from the Bernoulli distribution.</returns>
        public static int DoBernoulliTrial(double rnd, double p)
        {
            if (rnd < p)
            {
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// Generates one Bernoulli Trial.
        /// </summary>
        /// <param name="p">The probability of generating a one (between 0 and 1).</param>
        /// <returns>A random sample from the Bernoulli distribution.</returns>
        public static int DoBernoulliTrial(double p)
        {
            if (GetFloat(1) < p)
                return 1;
            return 0;
        }

        /// <summary>
        /// A simple flip coin trial. Returns 0 or 1 with equal probability.
        /// </summary>
        /// <returns>1 or 0, indicated the result of the flip coin.</returns>
        public static int FlipCoin()
        {
            return DoBernoulliTrial(0.5);
        }
    }
}

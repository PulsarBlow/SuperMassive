using System;
using System.Collections.Generic;

namespace SuperMassive
{
    /// <summary>
    /// Random Number Generator
    /// </summary>
    public static class RandomNumberGenerator
    {
        private static Random _random = new Random();

        /// <summary>
        /// Resets the seed used to generate the random numbers to a time-dependent one.
        /// </summary>
        public static void Seed()
        {
            _random = new Random();
        }
        /// <summary>
        /// Sets the seed used to generate the random numbers.
        /// </summary>
        /// <param name="seed"></param>
        public static void Seed(int seed)
        {
            _random = new Random(seed);
        }
        /// <summary>
        /// Gets a random int between 0 and max (half-inclusive)
        /// </summary>
        /// <param name="max">Maximum excluded value</param>
        /// <returns></returns>
        public static int Int(int max)
        {
            return _random.Next(max);
        }
        /// <summary>
        /// Gets a random int between min and max (half-inclusive)
        /// </summary>
        /// <param name="min">Minimum included value</param>
        /// <param name="max">Maximum excluded value</param>
        /// <returns></returns>
        public static int Int(int min, int max)
        {
            return Int(max - min) + min;
        }
        /// <summary>
        /// Gets a random int between 0 and max (full inclusive).
        /// </summary>
        /// <param name="max">Maximum included value</param>
        public static int IntInclusive(int max)
        {
            return _random.Next(max + 1);
        }

        /// <summary>
        /// Gets a random int between min and max (full inclusive).
        /// </summary>
        /// <param name="min">Minimum included value</param>
        /// <param name="max">Maximum included value</param>
        public static int IntInclusive(int min, int max)
        {
            return IntInclusive(max - min) + min;
        }
        /// <summary>
        /// Gets a random float between 0 and 1 (full inclusive)
        /// </summary>
        /// <returns></returns>
        public static float Float()
        {
            return Float(1);
        }
        /// <summary>
        /// Gets a random float between 0 and max (full inclusive)
        /// </summary>
        /// <param name="max">Maximum included value</param>
        public static float Float(float max)
        {
            if (max < 0.0f) throw new ArgumentOutOfRangeException("The max must be zero or greater.");

            return (float)_random.NextDouble() * max;
        }
        /// <summary>
        /// Gets a random float between min and max.
        /// </summary>
        /// <param name="min">Minimum included value</param>
        /// <param name="max">Maximum included value</param>
        public static float Float(float min, float max)
        {
            if (max < min) throw new ArgumentOutOfRangeException("The max must be min or greater.");

            return Float(max - min) + min;
        }
        /// <summary>
        /// Gets a random item from the given list.
        /// </summary>
        public static T Item<T>(IList<T> items)
        {
            return items[Int(items.Count)];
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
            if (RandomNumberGenerator.Float(1) < p)
                return 1;
            return 0;
        }
        /// <summary>
        /// A simple flip coin trial. Returns 0 or 1 with equal probability.
        /// </summary>
        /// <returns></returns>
        public static int FlipCoin()
        {
            return RandomNumberGenerator.DoBernoulliTrial(0.5);
        }
        /// <summary>
        /// Choose a random member of a set with a given chance of selection.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        // https://gist.github.com/ramazanpolat/5923486
        // chooseWithChance(1,99) will most probably (%99) return 1 since 99 is the second parameter.
        // chooseWithChance(99,1) will most probably (%99) return 0 since 99 is the first parameter.
        public static int ChooseWithChance(params int[] args)
        {
            int argCount = args.Length;
            int sumOfChances = 0;

            for (int i = 0; i < argCount; i++)
            {
                sumOfChances += args[i];
            }
            int randomInt = Int(sumOfChances);
            while ((randomInt -= args[argCount - 1]) > 0)
            {
                argCount--;
                sumOfChances -= args[argCount - 1];
            }

            return argCount - 1;
        }
    }
}

#nullable enable

namespace SuperMassive.Extensions
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Extension methods for integer types : <see cref="Int16"/>, <see cref="Int32"/>, see <see cref="Int64"/>
    /// </summary>
    public static class IntegerExtensions
    {
        /// <summary>
        /// Returns the current <see cref="short"/> as a <see cref="BitwiseMask"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static BitwiseMask AsMask(this short value)
        {
            return new BitwiseMask(value);
        }

        /// <summary>
        /// Returns the current <see cref="int"/> as a <see cref="BitwiseMask"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static BitwiseMask AsMask(this int value)
        {
            return new BitwiseMask(value);
        }

        /// <summary>
        /// Returns the current <see cref="long"/> as a <see cref="BitwiseMask"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static BitwiseMask AsMask(this long value)
        {
            return new BitwiseMask(value);
        }

        /// <summary>
        /// Returns a sequence of number starting from the current value
        /// </summary>
        /// <param name="startingValue"></param>
        /// <param name="endingValue"></param>
        /// <returns></returns>
        public static IEnumerable<int> To(this int startingValue, int endingValue)
        {
            if (endingValue == startingValue)
                return new[] { startingValue };
            switch (startingValue)
            {
                case int.MinValue when endingValue == int.MaxValue:
                    return new[] { startingValue };
                case int.MaxValue when endingValue == int.MinValue:
                    return new[] { startingValue };
            }

            var result = new List<int>();
            if (endingValue > startingValue)
            {
                for (int i = startingValue; i <= endingValue; i++)
                {
                    result.Add(i);
                }
            }
            else
            {
                for (int i = startingValue; i >= endingValue; i--)
                {
                    result.Add(i);
                }
            }

            return result;
        }
    }
}

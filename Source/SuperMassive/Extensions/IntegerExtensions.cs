using System;
using System.Collections.Generic;

namespace SuperMassive
{
    /// <summary>
    /// Extension methods for integer types : <see cref="Int16"/>, <see cref="Int32"/>, see <see cref="Int64"/>
    /// </summary>
    public static class IntegerExtensions
    {
        /// <summary>
        /// Returns the current <see cref="Int16"/> as a <see cref="BitwiseMask"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static BitwiseMask AsMask(this Int16 value)
        {
            return new BitwiseMask(value);
        }
        /// <summary>
        /// Returns the current <see cref="Int32"/> as a <see cref="BitwiseMask"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static BitwiseMask AsMask(this Int32 value)
        {
            return new BitwiseMask(value);
        }
        /// <summary>
        /// Returns the current <see cref="Int64"/> as a <see cref="BitwiseMask"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static BitwiseMask AsMask(this Int64 value)
        {
            return new BitwiseMask(value);
        }
        /// <summary>
        /// Returns a sequence of number
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static IEnumerable<int> To(this int from, int to)
        {
            if (to >= from)
            {
                for (int i = from; i <= to; i++)
                {
                    yield return i;
                }
            }
            else
            {
                for (int i = from; i >= to; i--)
                {
                    yield return i;
                }
            }
        }
    }
}

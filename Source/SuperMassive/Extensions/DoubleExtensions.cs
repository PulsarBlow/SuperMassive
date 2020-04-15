#nullable enable

namespace SuperMassive.Extensions
{
    using System;

    public static class DoubleExtensions
    {
        /// <summary>
        /// Compares two double values in a safe way (MISRA C++:2008 compliant)
        /// </summary>
        /// <param name="double1"></param>
        /// <param name="double2"></param>
        /// <param name="precision"></param>
        /// <returns></returns>
        public static bool AlmostEquals(this double double1, double double2, double precision = 0.0000001)
        {
            return Math.Abs(double1 - double2) <= precision;
        }
    }
}

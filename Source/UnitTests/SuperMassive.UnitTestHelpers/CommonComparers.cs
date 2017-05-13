using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace SuperMassive.UnitTestHelpers
{
    public static class CommonComparers
    {
        /// <summary>
        /// Compare two entity null references
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        public static void CheckNullReferences<T>(T expected, T actual) where T : class
        {
            if (expected == null && actual != null)
                Assert.Fail("Expected is null, actual is not null");
            if (expected != null && actual == null)
                Assert.Fail("Expected is not null, actual is null");
        }

        /// <summary>
        /// Compare two collection
        /// Compare length and execute the given delegate to compare each entry
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="comparer"></param>
        public static void AreCollectionEquals<T>(IEnumerable<T> expected, IEnumerable<T> actual, Action<T, T> comparer)
        {
            CheckNullReferences(expected, actual);
            if (expected == null)
                return;
            Assert.AreEqual(expected.Count(), actual.Count());
            for (int i = 0; i < expected.Count(); i++)
            {
                comparer(expected.ElementAt(i), actual.ElementAt(i));
            }
        }

        /// <summary>
        /// Check if two dates are similar (up to seconds)
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        public static void AreSimilar(DateTimeOffset expected, DateTimeOffset actual)
        {
            if (expected == DateTimeOffset.MinValue && actual != DateTimeOffset.MinValue)
                Assert.Fail("Expected is not set while actual has a value");
            if (expected != DateTimeOffset.MinValue && actual == DateTimeOffset.MinValue)
                Assert.Fail("Expected has a value while actual is not set");
            string dateFormat = "yyyy/MM/dd HH:mm:ss";
            Assert.AreEqual(expected.ToString(dateFormat), actual.ToString(dateFormat));
        }

        /// <summary>
        /// Check if two dates are similar (up to seconds)
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        public static void AreSimilar(DateTime expected, DateTime actual)
        {
            if (expected == DateTime.MinValue && actual != DateTime.MinValue)
                Assert.Fail("Expected is not set while actual has a value");
            if (expected != DateTime.MinValue && actual == DateTime.MinValue)
                Assert.Fail("Expected has a value while actual is not set");
            string dateFormat = "yyyy/MM/dd HH:mm:ss";
            Assert.AreEqual(expected.ToString(dateFormat), actual.ToString(dateFormat));
        }

        /// <summary>
        /// Check if two dates are similar (up to seconds)
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        public static void AreSimilar(DateTime? expected, DateTime? actual)
        {
            if (expected.HasValue && !actual.HasValue)
                Assert.Fail("Expected has no value while actual has one");
            if (!expected.HasValue && actual.HasValue)
                Assert.Fail("Expected has a value while actual has none");

            if (!expected.HasValue)
                return;

            if (expected.Value == DateTimeOffset.MinValue && actual.Value != DateTimeOffset.MinValue)
                Assert.Fail("Expected is not set while actual has a defined value");
            if (expected.Value != DateTimeOffset.MinValue && actual.Value == DateTimeOffset.MinValue)
                Assert.Fail("Expected has a defined value while actual is not set");
            string dateFormat = "yyyy/MM/dd HH:mm:ss";
            Assert.AreEqual(expected.Value.ToString(dateFormat), actual.Value.ToString(dateFormat));
        }
    }
}

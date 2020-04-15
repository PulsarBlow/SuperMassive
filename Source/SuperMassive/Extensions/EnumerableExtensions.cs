#nullable enable

namespace SuperMassive.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Provides extension methods for the generic IEnumerable interface
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Execute an lambda expression on each items of the collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="action"></param>
        public static void Each<T>(this IEnumerable<T> collection, Action<T> action)
        {
            Guard.ArgumentNotNull(collection, nameof(collection));
            Guard.ArgumentNotNull(action, nameof(action));

            foreach (var item in collection)
            {
                action(item);
            }
        }

        /// <summary>
        /// Joins all items in the current collection by calling ToString() on each.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string Join<T>(this IEnumerable<T> collection, string separator)
        {
            Guard.ArgumentNotNull(collection, nameof(collection));

            return string.Join(separator, collection);
        }

        /// <summary>
        /// Picks a random element from the current collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static T RandPick<T>(this IEnumerable<T> collection)
        {
            Guard.ArgumentNotNull(collection, nameof(collection));

            var items = collection.ToArray();
            return items[RandomNumberGenerator.Int(items.Length)];
        }

        /// <summary>
        /// Picks a range of random elements from the current collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="count">Number of item to pick</param>
        /// <returns></returns>
        public static IEnumerable<T> RangeRandPick<T>(this IEnumerable<T> collection, int count)
        {
            Guard.ArgumentNotNull(collection, nameof(collection));

            var items = collection.ToArray();
            for (var i = 0; i < count; i++)
                yield return items[RandomNumberGenerator.Int(items.Length)];
        }
    }
}

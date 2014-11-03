using System.Collections.Generic;

namespace SuperMassive
{
    /// <summary>
    /// Provides extension methods for the <see cref="List"/> class.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Adds a collection of items to the list if and only if the collection of items is not null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="items"></param>
        public static void AddRangeIfNotNull<T>(this List<T> list, IEnumerable<T> items)
        {
            if (items == null) return;
            list.AddRange(items);
        }
    }
}

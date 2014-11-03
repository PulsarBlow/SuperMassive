using System.Collections.Generic;

namespace SuperMassive
{
    /// <summary>
    /// Provides extension methods for the <see cref="ICollection"/> interface.
    /// </summary>
    public static class ICollectionExtensions
    {
        /// <summary>
        /// Adds an item to the collection if and only if the item is not null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="item"></param>
        public static void AddIfNotNull<T>(this ICollection<T> collection, T item)
            where T : class
        {
            if (item == null) return;
            collection.Add(item);
        }
    }
}

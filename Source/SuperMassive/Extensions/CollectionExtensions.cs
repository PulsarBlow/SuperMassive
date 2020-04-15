#nullable enable

namespace SuperMassive.Extensions
{
    using System.Collections.Generic;

    /// <summary>
    /// Provides extension methods for the <see cref="ICollection{T}"/> interface.
    /// </summary>
    public static class CollectionExtensions
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
            Guard.ArgumentNotNull(collection, nameof(collection));

            if (item == null)
                return;

            collection.Add(item);
        }
    }
}

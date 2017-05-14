namespace SuperMassive.Extensions
{
    using System.Collections.Generic;

    /// <summary>
    /// Provides extension methods for the <see cref="ICollection"/> interface.
    /// </summary>
    public static class ICollectionExtensions
    {
        /// <summary>
        /// Adds an item to the collection if and only if the item is not null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="extended"></param>
        /// <param name="item"></param>
        public static void AddIfNotNull<T>(this ICollection<T> extended, T item)
            where T : class
        {
            Guard.ArgumentNotNull(extended, nameof(extended));

            if (item == null)
                return;

            extended.Add(item);
        }
    }
}

namespace SuperMassive
{
    using System.Collections.Generic;

    /// <summary>
    /// A collection of <see cref="T"/> providing paging logic
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedCollection<T>
    {
        private readonly List<T> _items = new List<T>();

        /// <summary>
        /// Total length
        /// </summary>
        public long TotalLength { get; set; }

        /// <summary>
        /// Current page
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Current page length
        /// </summary>
        public int PageLength { get; set; }

        /// <summary>
        /// Returns true if the current page collection contains all the existing records
        /// </summary>
        public bool IsComplete => PageLength == TotalLength;

        /// <summary>
        /// Items
        /// </summary>
        public IReadOnlyList<T> Items => _items.AsReadOnly();

        /// <summary>
        /// Creates a new instance of the <see cref="PagedCollection{T}"/> class.
        /// </summary>
        public PagedCollection()
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="PagedCollection{T}"/> class.
        /// </summary>
        /// <param name="collection"></param>
        public PagedCollection(IEnumerable<T> collection)
        : this(0,0, 0, collection)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="PagedCollection{T}"/> class.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageLength"></param>
        /// <param name="totalLength"></param>
        /// <param name="collection"></param>
        public PagedCollection(int page, int pageLength, int totalLength, IEnumerable<T> collection)
        {
            Page = page;
            PageLength = pageLength;
            TotalLength = totalLength;

            _items.AddRange(collection);
        }
    }
}

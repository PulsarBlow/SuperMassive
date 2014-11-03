using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMassive
{
    /// <summary>
    /// A collection of <see cref="{T}"/> providing paging logic
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedCollection<T>
    {
        #region Properties
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
        /// Returns true if the current page collection contains all the existings records
        /// </summary>
        public bool IsComplete
        {
            get
            {
                return PageLength == TotalLength;
            }
        }
        /// <summary>
        /// Items
        /// </summary>
        public readonly List<T> Items = new List<T>();
        #endregion

        #region Constructors
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
        {
            if (collection != null)
                this.Items.AddRange(collection);
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
            this.Page = page;
            this.PageLength = pageLength;
            this.TotalLength = totalLength;
            if (collection != null)
                this.Items.AddRange(collection);
        }
        #endregion
    }
}

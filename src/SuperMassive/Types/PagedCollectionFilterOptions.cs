namespace SuperMassive
{
    /// <summary>
    /// Encapsulates filtering options for a <see cref="PagedCollection{T}"/>
    /// </summary>
    public class PagedCollectionFilterOptions
    {
        /// <summary>
        /// Page number
        /// </summary>
        public int? Page { get; set; }
        /// <summary>
        /// Page length
        /// </summary>
        public int? PageLength { get; set; }
    }
}

using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Globalization;

namespace SuperMassive.Storage.TableStorage.Queries
{
    /// <summary>
    /// Select the top N entity matching a given partition key
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class TopEntriesForPartition<TEntity> : TableStorageQuery<TEntity>
        where TEntity : ITableEntity, new()
    {
        public const int DefaultLength = 100;

        private readonly string _partition;
        private readonly int _take;
        private readonly string _cacheKey;

        public override string UniqueIdentifier
        {
            get { return _cacheKey; }
        }

        public TopEntriesForPartition(string partition)
            : this(partition, DefaultLength)
        { }
        public TopEntriesForPartition(string partition, int take)
            : base()
        {
            Guard.ArgumentNotNullOrWhiteSpace(partition, "partition");

            _partition = partition;
            _take = take;
            _cacheKey = CreateCacheKey();
        }

        protected override TableQuery<TEntity> CreateQuery()
        {
            string filterCondition = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, _partition.ToUpperInvariant());
            TableQuery<TEntity> query = new TableQuery<TEntity>();
            return query.Where(filterCondition).Take(_take);
        }

        protected string CreateCacheKey()
        {
            return String.Format(CultureInfo.InvariantCulture,
                    "TopEntriesForPartitionQuery-{0}-{1}", _partition, _take);
        }
    }
}

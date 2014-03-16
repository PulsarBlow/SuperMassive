using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace SuperMassive.Storage.TableStorage
{
    public class TableStorageQueryCache<TEntity> : ITableStorageQuery<TEntity>
        where TEntity : ITableEntity, new()
    {
        public const string DefaultCacheName = "TableStorageQueryCache";

        private readonly ITableStorageQuery<TEntity> _query;
        private readonly CacheItemPolicy _cacheItemPolicy;
        private readonly string _cacheKey = String.Empty;
        private readonly static MemoryCache Cache = new MemoryCache(DefaultCacheName);

        public string UniqueIdentifier
        {
            get { return _query.UniqueIdentifier; }
        }

        public TableStorageQueryCache(ITableStorageQuery<TEntity> query)
            : this(query, null)
        { }

        public TableStorageQueryCache(ITableStorageQuery<TEntity> query, string cacheKey, CacheItemPolicy policy = null)
        {
            Guard.ArgumentNotNull(query, "query");
            Guard.ArgumentNotNullOrWhiteSpace(cacheKey, "cacheKey");

            _query = query;
            _cacheKey = cacheKey;

            if (policy == null)
            {
                _cacheItemPolicy = TableStorageConfiguration.DefaultQueryCacheItemPolicy();
            }
            else
            {
                _cacheItemPolicy = policy;
            }
        }

        public async Task<ICollection<TEntity>> Execute(CloudTable table)
        {
            Guard.ArgumentNotNull(table, "table");

            string cacheKey = CreateCacheKey(table);
            if (Cache.Contains(cacheKey))
            {
                return Cache.Get(cacheKey) as ICollection<TEntity>;
            }

            var result = await _query.Execute(table).ConfigureAwait(false);
            Cache.Add(new CacheItem(cacheKey, result), _cacheItemPolicy);
            return result;
        }

        private string CreateCacheKey(CloudTable table)
        {
            return CryptographyHelper.ComputeSHA1Hash(String.Format(CultureInfo.InvariantCulture,
                "{0}-{1}-{2}", _query.UniqueIdentifier, _cacheKey, table.Uri));
        }
    }
}

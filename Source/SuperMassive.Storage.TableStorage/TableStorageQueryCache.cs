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
        private static ObjectCache Cache;

        public string UniqueIdentifier
        {
            get { return _query.UniqueIdentifier; }
        }

        public TableStorageQueryCache(ITableStorageQuery<TEntity> query)
            : this(query, String.Empty, null, null)
        { }
        public TableStorageQueryCache(ITableStorageQuery<TEntity> query, string cacheKey)
            : this(query, cacheKey, null, null)
        { }
        public TableStorageQueryCache(ITableStorageQuery<TEntity> query, string cacheKey, ObjectCache cache)
            : this(query, cacheKey, cache, null)
        { }
        public TableStorageQueryCache(ITableStorageQuery<TEntity> query, string cacheKey, CacheItemPolicy policy)
            : this(query, cacheKey, null, policy)
        { }
        public TableStorageQueryCache(ITableStorageQuery<TEntity> query, string cacheKey, ObjectCache cache, CacheItemPolicy policy)
        {
            Guard.ArgumentNotNull(query, "query");
            Guard.ArgumentNotNull(cacheKey, "cacheKey");

            _query = query;
            _cacheKey = cacheKey;

            if (cacheKey == null)
            {
                Cache = new MemoryCache(DefaultCacheName);
            }
            else
            {
                Cache = cache;
            }

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

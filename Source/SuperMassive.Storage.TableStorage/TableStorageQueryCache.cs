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
        private static ObjectCache Cache;

        private readonly ITableStorageQuery<TEntity> _query;
        private readonly CacheItemPolicy _cacheItemPolicy;

        public string UniqueIdentifier
        {
            get { return _query.UniqueIdentifier; }
        }

        public TableStorageQueryCache(ITableStorageQuery<TEntity> query)
            : this(query, null, null)
        { }
        public TableStorageQueryCache(ITableStorageQuery<TEntity> query, ObjectCache cache)
            : this(query, cache, null)
        { }
        public TableStorageQueryCache(ITableStorageQuery<TEntity> query, CacheItemPolicy policy)
            : this(query, null, policy)
        { }
        public TableStorageQueryCache(ITableStorageQuery<TEntity> query, ObjectCache cache, CacheItemPolicy policy)
        {
            Guard.ArgumentNotNull(query, "query");

            _query = query;

            if (cache == null)
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
                "{0}-{1}", table.Uri, _query.UniqueIdentifier));
        }
    }
}

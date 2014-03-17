using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace SuperMassive.Storage.TableStorage
{
    public class TableStorageReader : TableStorageProvider
    {
        private CacheItemPolicy _cachePolicy;
        private string _cacheKey = String.Empty;

        public TableStorageReader(string tableName, string connectionStringSettingName)
            : base(tableName, connectionStringSettingName)
        { }

        public async Task<ICollection<TEntity>> ExecuteAsync<TEntity>(ITableStorageQuery<TEntity> query)
            where TEntity : ITableEntity, new()
        {
            Guard.ArgumentNotNull(query, "query");

            return await Task.Run(() =>
            {
                if (_cachePolicy == null)
                    return query.Execute(_table);

                TableStorageQueryCache<TEntity> cachedQuery = new TableStorageQueryCache<TEntity>(query, _cacheKey, policy: _cachePolicy);
                return cachedQuery.Execute(_table);
            });
        }

        public TableStorageReader WithCache()
        {
            return WithCache(TableStorageConfiguration.DefaultQueryCacheItemPolicy());
        }
        public TableStorageReader WithCache(CacheItemPolicy policy)
        {
            return WithCache(policy, String.Empty);
        }
        public TableStorageReader WithCache(CacheItemPolicy policy, string cacheKey)
        {
            Guard.ArgumentNotNull(policy, "policy");
            Guard.ArgumentNotNull(cacheKey, "cacheKey");

            _cachePolicy = policy;
            _cacheKey = cacheKey;

            return this;
        }

        public static TableStorageReader Table(string tableName, string connectionStringSettingName)
        {
            return new TableStorageReader(tableName, connectionStringSettingName);
        }
    }
}

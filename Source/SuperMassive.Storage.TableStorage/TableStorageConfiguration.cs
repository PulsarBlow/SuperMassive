using Microsoft.WindowsAzure.Storage.RetryPolicies;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Runtime.Caching;

namespace SuperMassive.Storage.TableStorage
{
    public class TableStorageConfiguration
    {
        public static TableRequestOptions DefaultRequestOptions()
        {
            return new TableRequestOptions
            {
                PayloadFormat = TablePayloadFormat.Json,
                RetryPolicy = new ExponentialRetry()
            };
        }

        public static CacheItemPolicy DefaultQueryCacheItemPolicy()
        {
            return new CacheItemPolicy
            {
                AbsoluteExpiration = DateTime.UtcNow.Add(TimeSpan.FromMinutes(1))
            };
        }
    }
}

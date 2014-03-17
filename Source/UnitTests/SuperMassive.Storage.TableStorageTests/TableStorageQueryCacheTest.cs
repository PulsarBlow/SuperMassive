using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using SuperMassive.Storage.TableStorage;
using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace SuperMassive.Storage.TableStorageTests
{
    [TestClass]
    public class TableStorageQueryCacheTest
    {

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateInstance_WithNullQueryAndNullCacheKey_WithException()
        {
            TableStorageQueryCache<TableEntity> queryCache = new TableStorageQueryCache<TableEntity>(null, null);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateInstance_WithNullQuery_WithException()
        {
            TableStorageQueryCache<TableEntity> queryCache = new TableStorageQueryCache<TableEntity>(null);
        }
        [TestMethod]
        public void CreateInstance_WithAllParameters_WithSuccess()
        {
            TableStorageQueryCache<TableEntity> queryCache = new TableStorageQueryCache<TableEntity>(
                new EmptyQuery(),
                CryptographyHelper.ComputeSHA1Hash(Guid.NewGuid().ToString()),
                new MemoryCache(CryptographyHelper.ComputeSHA1Hash(Guid.NewGuid().ToString())),
                new CacheItemPolicy());
        }
        [TestMethod]
        public async Task ExecuteAsync_WithSuccess()
        {
            TableStorageQueryCache<TableEntity> queryCache = new TableStorageQueryCache<TableEntity>(
                new EmptyQuery(),
                CryptographyHelper.ComputeSHA1Hash(Guid.NewGuid().ToString()),
                new MemoryCache(CryptographyHelper.ComputeSHA1Hash(Guid.NewGuid().ToString())),
                new CacheItemPolicy());
            CloudTable table = CreateTestTable();
            await queryCache.Execute(table);
        }
        public CloudTable CreateTestTable()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.DevelopmentStorageAccount;
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("TableStorageQueryCacheTest");
            table.CreateIfNotExists();
            return table;
        }

        private class EmptyQuery : ITableStorageQuery<TableEntity>
        {

            public Task<ICollection<TableEntity>> Execute(CloudTable table)
            {
                return Task.FromResult<ICollection<TableEntity>>(new List<TableEntity>());
            }

            public string UniqueIdentifier
            {
                get { return CryptographyHelper.ComputeSHA1Hash(Guid.NewGuid().ToString()); }
            }
        }
    }
}

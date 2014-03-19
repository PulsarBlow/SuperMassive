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
            TableStorageQueryCache<TableEntity> queryCache = new TableStorageQueryCache<TableEntity>(null);
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
                new MemoryCache(CryptographyHelper.ComputeSHA1Hash(Guid.NewGuid().ToString())),
                new CacheItemPolicy());
        }
        [TestMethod]
        public async Task ExecuteAsync_WithSuccess()
        {
            TableStorageQueryCache<TableEntity> queryCache = new TableStorageQueryCache<TableEntity>(
                new EmptyQuery(),
                new MemoryCache(CryptographyHelper.ComputeSHA1Hash(Guid.NewGuid().ToString())),
                new CacheItemPolicy());
            CloudTable table = CreateTestTable();
            await queryCache.Execute(table);
        }

        [TestMethod]
        public async Task ExecuteAsync_WithCache_WithSuccess()
        {
            RandomResultQuery query = new RandomResultQuery();
            TableStorageQueryCache<TableEntity> queryCache = new TableStorageQueryCache<TableEntity>(query);
            CloudTable table = CreateTestTable();
            var expected = await queryCache.Execute(table);
            var actual = await queryCache.Execute(table);
            CollectionAssert(expected, actual);
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
                get { return "EmptyQuery"; }
            }
        }
        private class RandomResultQuery : ITableStorageQuery<TableEntity>
        {
            string _partitionKey = Randomizer.RandomString(15);

            public Task<ICollection<TableEntity>> Execute(CloudTable table)
            {
                return Task.FromResult<ICollection<TableEntity>>(new List<TableEntity>
                {
                    new TableEntity(_partitionKey, Randomizer.RandomString(20)),
                    new TableEntity(_partitionKey, Randomizer.RandomString(20)),
                    new TableEntity(_partitionKey, Randomizer.RandomString(20))
                });
            }

            public string UniqueIdentifier
            {
                get { return "SimpleQuery"; }
            }
        }

        private void CollectionAssert(ICollection<TableEntity> expected, ICollection<TableEntity> actual)
        {
            if (expected == null && actual != null)
                Assert.Fail();
            if (expected != null && actual == null)
                Assert.Fail();
            Assert.AreEqual(expected.Count, actual.Count);
        }
    }
}

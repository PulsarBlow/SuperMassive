using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SuperMassive.Storage.TableStorage
{
    public class TableStorageWriter : TableStorageProvider
    {
        private const int BatchSize = 100;
        private readonly ConcurrentQueue<Tuple<ITableEntity, TableOperation>> _operations;

        public int PendingOperations
        {
            get { return _operations.Count; }
        }

        public TableStorageWriter(string tableName, string connectionStringSettingName)
            : base(tableName, connectionStringSettingName)
        {
            _operations = new ConcurrentQueue<Tuple<ITableEntity, TableOperation>>();
        }

        public void Insert<TEntity>(TEntity entity)
            where TEntity : ITableEntity
        {
            _operations.Enqueue(new Tuple<ITableEntity, TableOperation>(
                entity,
                TableOperation.Insert(entity)));
        }

        public void Delete<TEntity>(TEntity entity)
            where TEntity : ITableEntity
        {
            _operations.Enqueue(new Tuple<ITableEntity, TableOperation>(
                entity,
                TableOperation.Delete(entity)));
        }

        public void InsertOrMerge<TEntity>(TEntity entity)
            where TEntity : ITableEntity
        {
            _operations.Enqueue(new Tuple<ITableEntity, TableOperation>(
                entity,
                TableOperation.InsertOrMerge(entity)));
        }

        public void InsertOrReplace<TEntity>(TEntity entity)
            where TEntity : ITableEntity
        {
            _operations.Enqueue(new Tuple<ITableEntity, TableOperation>(
                entity,
                TableOperation.InsertOrReplace(entity)));
        }

        public void Merge<TEntity>(TEntity entity)
            where TEntity : ITableEntity
        {
            _operations.Enqueue(new Tuple<ITableEntity, TableOperation>(
                entity,
                TableOperation.Merge(entity)));
        }

        public void Replace<TEntity>(TEntity entity)
            where TEntity : ITableEntity
        {
            _operations.Enqueue(new Tuple<ITableEntity, TableOperation>(
                entity,
                TableOperation.Replace(entity)));
        }

        public void Execute()
        {
            _table.CreateIfNotExists();

            var workload = CreateWorkload(_operations.Count)
                .GroupBy(x => x.Item1.PartitionKey)
                .ToList();

            foreach (var item in workload)
            {
                var operations = item.ToList();
                int batchId = 0;
                var operationBatch = GetOperationsInBatch(operations, batchId);
                while (operationBatch.Any())
                {
                    TableBatchOperation batchOperation = CreateBatch(operationBatch);
                    OperationContext operationContext = new OperationContext();
                    ExecuteBatchWithRetries(batchOperation);

                    batchId++;
                    operationBatch = GetOperationsInBatch(operations, batchId);
                }
            }
        }
        public async Task ExecuteAsync()
        {
            await _table.CreateIfNotExistsAsync();

            var workload = CreateWorkload(_operations.Count)
                .GroupBy(x => x.Item1.PartitionKey)
                .ToList();

            foreach (var item in workload)
            {
                var operations = item.ToList();
                int batchId = 0;
                var operationBatch = GetOperationsInBatch(operations, batchId);
                while (operationBatch.Any())
                {
                    TableBatchOperation batchOperation = CreateBatch(operationBatch);
                    OperationContext operationContext = new OperationContext();
                    await ExecuteBatchWithRetriesAsync(batchOperation, operationContext, new CancellationToken());

                    batchId++;
                    operationBatch = GetOperationsInBatch(operations, batchId);
                }
            }
        }

        private void ExecuteBatchWithRetries(TableBatchOperation batchOperation)
        {
            TableRequestOptions options = TableStorageConfiguration.DefaultRequestOptions();
            _table.ExecuteBatch(batchOperation, options);
        }

        private async Task ExecuteBatchWithRetriesAsync(TableBatchOperation batch, OperationContext context, CancellationToken cancellationToken)
        {
            TableRequestOptions requestOptions = TableStorageConfiguration.DefaultRequestOptions();
            await _table.ExecuteBatchAsync(batch, requestOptions, context, cancellationToken);
        }

        private List<Tuple<ITableEntity, TableOperation>> CreateWorkload(int quantity)
        {
            List<Tuple<ITableEntity, TableOperation>> workload = new List<Tuple<ITableEntity, TableOperation>>();
            for (int i = 0; i < quantity; i++)
            {
                Tuple<ITableEntity, TableOperation> operation;
                _operations.TryDequeue(out operation);
                if (operation != null) { workload.Add(operation); }
            }
            return workload;
        }


        static TableBatchOperation CreateBatch(List<Tuple<ITableEntity, TableOperation>> operations)
        {
            TableBatchOperation batch = new TableBatchOperation();
            operations.ForEach(x => batch.Add(x.Item2));
            return batch;
        }
        static List<Tuple<ITableEntity, TableOperation>> GetOperationsInBatch(
            IEnumerable<Tuple<ITableEntity, TableOperation>> operations,
            int batchId)
        {
            return operations
                .Skip(batchId * BatchSize)
                .Take(BatchSize)
                .ToList();
        }

    }
}
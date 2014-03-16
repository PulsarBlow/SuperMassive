using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Globalization;

namespace SuperMassive.Storage.TableStorage.Queries
{
    public class SelectAzureDiagnosticsByDateRange<TEntity> : TableStorageQuery<TEntity>
        where TEntity : ITableEntity, new()
    {
        private readonly string _tableStartPartition;
        private readonly string _tableEndPartition;
        private readonly string _cacheKey;

        public override string UniqueIdentifier
        {
            get { return _cacheKey; }
        }

        public SelectAzureDiagnosticsByDateRange(DateTime dateStart, DateTime dateEnd)
            : base()
        {
            _tableStartPartition = "0" + dateStart.ToUniversalTime().Ticks;
            _tableEndPartition = "0" + dateEnd.ToUniversalTime().Ticks;
            _cacheKey = CreateCacheKey();
        }

        protected override TableQuery<TEntity> CreateQuery()
        {
            string filterCondition = CreateFilterCondition();
            TableQuery<TEntity> query = new TableQuery<TEntity>();
            return query.Where(filterCondition);
        }

        protected string CreateCacheKey()
        {
            return String.Format(CultureInfo.InvariantCulture,
                    "SelectAzureDiagnosticsByDateRange-{0}-{1}", _tableStartPartition, _tableEndPartition);
        }

        private string CreateFilterCondition()
        {
            string from = TableQuery.GenerateFilterCondition("PartitionKey",
                QueryComparisons.GreaterThanOrEqual,
                _tableStartPartition.ToUpperInvariant());

            string to = TableQuery.GenerateFilterCondition("PartitionKey",
                QueryComparisons.LessThanOrEqual,
                _tableEndPartition.ToUpperInvariant());

            return TableQuery.CombineFilters(from, TableOperators.And, to);
        }
    }
}

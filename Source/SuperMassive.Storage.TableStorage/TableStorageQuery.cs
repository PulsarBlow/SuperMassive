using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperMassive.Storage.TableStorage
{
    public abstract class TableStorageQuery<TEntity> :
        IModelQuery<Task<ICollection<TEntity>>, CloudTable>,
        ITableStorageQuery<TEntity>
            where TEntity : ITableEntity, new()
    {
        /// <summary>
        /// Execute the query against the given <see cref="CloudTable"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual Task<ICollection<TEntity>> Execute(CloudTable model)
        {
            Guard.ArgumentNotNull(model, "model");

            return Task.Run(() =>
            {
                TableQuery<TEntity> query = CreateQuery();
                return (ICollection<TEntity>)model.ExecuteQuery(query, TableStorageConfiguration.DefaultRequestOptions()).ToList();
            });
        }

        /// <summary>
        /// Concrete implementation of the query
        /// </summary>
        /// <returns></returns>
        protected abstract TableQuery<TEntity> CreateQuery();

        public abstract string UniqueIdentifier { get; }
    }
}

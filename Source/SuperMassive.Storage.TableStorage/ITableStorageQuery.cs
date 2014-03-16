using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperMassive.Storage.TableStorage
{
    public interface ITableStorageQuery<TEntity>
        where TEntity : ITableEntity, new()
    {
        Task<ICollection<TEntity>> Execute(CloudTable table);
        string UniqueIdentifier { get; }
    }
}

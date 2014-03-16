
namespace SuperMassive.Storage.TableStorage
{
    public interface IPartitionKeyResolver<T>
    {
        string Resolve(T entityId);
    }
}

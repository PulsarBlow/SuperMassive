
namespace SuperMassive.TableStorage
{
    public interface IPartitionKeyResolver<T>
    {
        string Resolve(T entityId);
    }
}

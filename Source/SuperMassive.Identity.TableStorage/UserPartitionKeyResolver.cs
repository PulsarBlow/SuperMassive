using SuperMassive.TableStorage;
using System;

namespace SuperMassive.Identity.TableStorage
{
    /// <summary>
    /// Encapsulates the partition key generation logic
    /// </summary>
    public class UserPartitionKeyResolver : IPartitionKeyResolver<string>
    {
        /// <summary>
        /// The default partition key prefix
        /// </summary>
        public const string DefaultPrefix = "User";
        /// <summary>
        /// The default number of partition shards
        /// Used to distribute the users evently accross this number of partitions
        /// </summary>
        public const int DefaultNumberOfBuckets = 100;

        private int _numberOfBuckets;
        private string _partitionPrefix;

        /// <summary>
        /// Create a new instance of the <see cref="UserPartitionKeyResolver"/> class.
        /// </summary>
        public UserPartitionKeyResolver()
            : this(DefaultNumberOfBuckets, DefaultPrefix)
        { }
        /// <summary>
        /// Create a new instance of the <see cref="UserPartitionKeyResolver"/> class.
        /// </summary>
        /// <param name="numberOfBuckets"></param>
        public UserPartitionKeyResolver(int numberOfBuckets)
            : this(numberOfBuckets, DefaultPrefix)
        { }
        /// <summary>
        /// Create a new instance of the <see cref="UserPartitionKeyResolver"/> class.
        /// </summary>
        /// <param name="partitionPrefix"></param>
        public UserPartitionKeyResolver(string partitionPrefix)
            : this(DefaultNumberOfBuckets, partitionPrefix)
        { }
        /// <summary>
        /// Create a new instance of the <see cref="UserPartitionKeyResolver"/> class.
        /// </summary>
        /// <param name="partitionPrefix"></param>
        public UserPartitionKeyResolver(int numberOfBuckets, string partitionPrefix)
        {
            if (numberOfBuckets <= 0)
                throw new ArgumentException("Number of buckets is out of range", "numberOfBuckets");
            _numberOfBuckets = numberOfBuckets;
            _partitionPrefix = partitionPrefix;
        }

        /// <summary>
        /// Resolve a user id into a partition key
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public string Resolve(string entityId)
        {
            Guard.ArgumentNotNullOrWhiteSpace(entityId, "entityId");

            return String.Format("{0}{1}{2}",
                _partitionPrefix,
                String.IsNullOrWhiteSpace(_partitionPrefix) ? "" : "_",
                (Math.Abs(entityId.GetHashCode()) % _numberOfBuckets) + 1);
        }
    }
}

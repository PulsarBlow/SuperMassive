using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace SuperMassive.Storage.TableStorage
{
    public class DynamicEntity : TableEntity
    {
        private IDictionary<string, EntityProperty> _properties = new Dictionary<string, EntityProperty>();
        private IDictionary<string, Type> _knownTypes = new Dictionary<string, Type>();

        public DynamicEntity(string partitionKey, DateTime dateTime)
        {
            Guard.ArgumentNotNullOrWhiteSpace(partitionKey, "partitionKey");

            PartitionKey = partitionKey.ToUpperInvariant();
            // Descending order - Newest first
            RowKey = String.Format(CultureInfo.InvariantCulture, "{0}-{1}",
                DateTime.MaxValue.Subtract(dateTime).TotalMilliseconds.ToString(CultureInfo.InvariantCulture),
                Guid.NewGuid());
        }
        public DynamicEntity(string partitionKey, string rowKey)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
        }
        public DynamicEntity()
        { }

        public override void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext operationContext)
        {
            _properties = properties;
        }

        public void Add(string key, EntityProperty value)
        {
            _properties.Add(key, value);
        }
        public void Add(string key, bool value)
        {
            _properties.Add(key, new EntityProperty(value));
        }
        public void Add(string key, byte[] value)
        {
            _properties.Add(key, new EntityProperty(value));
        }
        public void Add(string key, DateTime? value)
        {
            _properties.Add(key, new EntityProperty(value));
        }
        public void Add(string key, DateTimeOffset? value)
        {
            _properties.Add(key, new EntityProperty(value));
        }
        public void Add(string key, double value)
        {
            _properties.Add(key, new EntityProperty(value));
        }
        public void Add(string key, Guid value)
        {
            _properties.Add(key, new EntityProperty(value));
        }
        public void Add(string key, int value)
        {
            _properties.Add(key, new EntityProperty(value));
        }
        public void Add(string key, long value)
        {
            _properties.Add(key, new EntityProperty(value));
        }
        public void Add(string key, string value)
        {
            _properties.Add(key, new EntityProperty(value));
        }

        /// <summary>
        /// Add complex types
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add<T>(string key, T value)
            where T : new()
        {
            if (!_knownTypes.ContainsKey(key))
                _knownTypes.Add(key, typeof(T));
            string json = JsonConvert.SerializeObject(value);
            _properties.Add(key, new EntityProperty(json));

        }
    }
}

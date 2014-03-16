using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Net;

namespace SuperMassive.Storage.TableStorage
{
    public abstract class TableStorageProvider
    {
        protected readonly CloudTable _table;

        protected TableStorageProvider(string tableName, string connectionStringSettingName)
        {
            Guard.ArgumentNotNullOrWhiteSpace(tableName, "tableName");
            Guard.ArgumentNotNullOrWhiteSpace(connectionStringSettingName, "connectionStringSettingName");

            string connectionString = CloudConfigurationManager.GetSetting(connectionStringSettingName);
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            // http://msmvps.com/blogs/nunogodinho/archive/2013/11/20/windows-azure-storage-performance-best-practices.aspx
            ServicePointManager.FindServicePoint(storageAccount.TableEndpoint).UseNagleAlgorithm = false;
            _table = GetTableReference(storageAccount, tableName);
        }

        protected CloudTable GetTableReference(CloudStorageAccount storageAccount, string tableName)
        {
            CloudTableClient client = storageAccount.CreateCloudTableClient();
            return client.GetTableReference(tableName);
        }
    }
}

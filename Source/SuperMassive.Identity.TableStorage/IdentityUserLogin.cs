using Microsoft.AspNet.Identity;
using Microsoft.WindowsAzure.Storage.Table;
using System.Web;

namespace SuperMassive.Identity.TableStorage
{
    public class IdentityUserLogin : TableEntity
    {
        public string UserId { get; set; }

        public string LoginProvider
        {
            get
            {
                return PartitionKey;
            }
            set
            {
                PartitionKey = value;
            }
        }

        public string ProviderKey
        {
            get { return RowKey; }
            set { RowKey = value; }
        }

        public IdentityUserLogin()
        {

        }
        public IdentityUserLogin(string userId, string loginProvider, string providerKey)
            : this(userId, new UserLoginInfo(loginProvider, providerKey))
        { }
        public IdentityUserLogin(string userId, UserLoginInfo loginInfo)
        {
            //Guard.ArgumentNotNullOrWhiteSpace(userId, "userId");
            Guard.ArgumentNotNull(loginInfo, "loginInfo");

            UserId = userId;
            this.PartitionKey = EncodeKey(loginInfo.LoginProvider);
            this.RowKey = EncodeKey(loginInfo.ProviderKey);
        }

        private static string EncodeKey(string key)
        {
            return HttpUtility.UrlEncode(key);
        }
    }
}

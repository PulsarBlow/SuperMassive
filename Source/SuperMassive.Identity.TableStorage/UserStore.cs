using Microsoft.AspNet.Identity;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using SuperMassive.Storage.TableStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SuperMassive.Identity.TableStorage
{
    /// <summary>
    /// UserStore implementation for Azure Table Storage
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public class UserStore<TUser> :
        IUserStore<TUser>,
        IUserPasswordStore<TUser>,
        IUserLoginStore<TUser>,
        IUserRoleStore<TUser>,
        IUserEmailStore<TUser>,
        IUserSecurityStampStore<TUser>,
        IUserClaimStore<TUser>
        where TUser : IdentityUser
    {
        private bool _disposed = false;
        private readonly IPartitionKeyResolver<string> _partitionKeyResolver;
        private readonly CloudTable _userTableReference;
        private readonly CloudTable _loginTableReference;

        public UserStore(string storageConnectionString)
            : this(storageConnectionString, new UserPartitionKeyResolver())
        { }
        public UserStore(string storageConnectionString, IPartitionKeyResolver<string> partitionKeyResolver)
        {
            Guard.ArgumentNotNull(storageConnectionString, "storageConnectionString");
            Guard.ArgumentNotNull(partitionKeyResolver, "partitionKeyResolver");

            _partitionKeyResolver = partitionKeyResolver;

            var tableClient = GetTableClient(storageConnectionString);

            _userTableReference = tableClient.GetTableReference("Users");
            _userTableReference.CreateIfNotExists();

            _loginTableReference = tableClient.GetTableReference("Logins");
            _loginTableReference.CreateIfNotExists();
        }

        #region IUserStore
        // This method doesn't perform uniqueness. That's the responsability of the session provider.
        public async Task CreateAsync(TUser user)
        {
            Guard.ArgumentNotNull(user, "user");

            user.PartitionKey = _partitionKeyResolver.Resolve(user.Id);
            var operation = TableOperation.Insert(user);
            await GetUserTable().ExecuteAsync(operation);
        }

        public async Task UpdateAsync(TUser user)
        {
            Guard.ArgumentNotNull(user, "user");
            var partitionKey = _partitionKeyResolver.Resolve(user.Id);
            user.PartitionKey = partitionKey;
            await UpdateUser(user);
        }

        public async Task<TUser> FindByIdAsync(string userId)
        {
            Guard.ArgumentNotNullOrWhiteSpace(userId, "userId");

            var partitionKey = _partitionKeyResolver.Resolve(userId);
            var operation = TableOperation.Retrieve<TUser>(partitionKey, userId);
            TableResult result = await GetUserTable().ExecuteAsync(operation);
            return (TUser)result.Result;
        }

        public Task<TUser> FindByNameAsync(string userName)
        {
            Guard.ArgumentNotNullOrWhiteSpace(userName, "userName");

            return FindByIdAsync(userName);
        }

        public async Task DeleteAsync(TUser user)
        {
            Guard.ArgumentNotNull(user, "user");

            user.ETag = "*";
            var operation = TableOperation.Delete(user);
            await GetUserTable().ExecuteAsync(operation);
        }

        #endregion

        #region IUserLoginStore
        public async Task AddLoginAsync(TUser user, UserLoginInfo login)
        {
            Guard.ArgumentNotNull(user, "user");
            Guard.ArgumentNotNull(login, "login");

            user.Logins.Add(new IdentityUserLogin(user.Id, login));
            await UpdateUser(user);

            var operation = TableOperation.Insert(new IdentityUserLogin(user.Id, login));
            await GetLoginTable().ExecuteAsync(operation);
        }

        public async Task<TUser> FindAsync(UserLoginInfo login)
        {
            Guard.ArgumentNotNull(login, "login");

            var lie = new IdentityUserLogin("", login);
            var operation = TableOperation.Retrieve<IdentityUserLogin>(lie.PartitionKey, lie.RowKey);
            var result = await GetLoginTable().ExecuteAsync(operation);
            var loginInfoEntity = (IdentityUserLogin)result.Result;
            if (loginInfoEntity == null)
                return null;
            return await FindByIdAsync(loginInfoEntity.UserId);
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user)
        {
            Guard.ArgumentNotNull(user, "user");

            return Task.FromResult<IList<UserLoginInfo>>(user.Logins.Select(x => new UserLoginInfo(x.LoginProvider, x.ProviderKey)).ToList());
        }

        public async Task RemoveLoginAsync(TUser user, UserLoginInfo login)
        {
            Guard.ArgumentNotNull(user, "user");
            Guard.ArgumentNotNull(login, "login");

            IdentityUserLogin identityLogin = user.Logins.FirstOrDefault(x => x.LoginProvider == login.LoginProvider && x.ProviderKey == login.ProviderKey);
            if (identityLogin != null)
            {
                user.Logins.Remove(identityLogin);
                await UpdateUser(user);
                var operation = TableOperation.Delete(new IdentityUserLogin(user.Id, login) { ETag = "*" });
                await GetLoginTable().ExecuteAsync(operation);
            }
        }
        #endregion

        #region IUserRoleStore
        public async Task AddToRoleAsync(TUser user, string roleName)
        {
            Guard.ArgumentNotNull(user, "user");
            Guard.ArgumentNotNullOrWhiteSpace(roleName, "roleName");

            if (!user.Roles.Contains(roleName))
            {
                user.Roles.Add(roleName);
                await UpdateUser(user);
            }
        }

        public Task<IList<string>> GetRolesAsync(TUser user)
        {
            Guard.ArgumentNotNull(user, "user");

            return Task.FromResult(user.Roles);
        }

        public Task<bool> IsInRoleAsync(TUser user, string roleName)
        {
            Guard.ArgumentNotNull(user, "user");
            Guard.ArgumentNotNullOrWhiteSpace(roleName, "roleName");

            return Task.FromResult(user.Roles.Contains(roleName));
        }

        public async Task RemoveFromRoleAsync(TUser user, string roleName)
        {
            Guard.ArgumentNotNull(user, "user");
            Guard.ArgumentNotNullOrWhiteSpace(roleName, "roleName");

            if (user.Roles.Remove(roleName))
            {
                await UpdateUser(user);
            }
        }
        #endregion

        #region IUserPasswordStore
        public Task<string> GetPasswordHashAsync(TUser user)
        {
            Guard.ArgumentNotNull(user, "user");
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(TUser user)
        {
            Guard.ArgumentNotNull(user, "user");
            return Task.FromResult(!String.IsNullOrWhiteSpace(user.PasswordHash));
        }

        public Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            Guard.ArgumentNotNull(user, "user");
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }
        #endregion

        #region IUserSecurityStampStore
        public Task<string> GetSecurityStampAsync(TUser user)
        {
            Guard.ArgumentNotNull(user, "user");

            return Task.FromResult<string>(user.SecurityStamp);
        }

        public Task SetSecurityStampAsync(TUser user, string stamp)
        {
            Guard.ArgumentNotNull(user, "user");
            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }
        #endregion

        #region IUserClaimStore
        public Task AddClaimAsync(TUser user, Claim claim)
        {
            Guard.ArgumentNotNull(user, "user");
            Guard.ArgumentNotNull(claim, "claim");

            user.Claims.Add(new IdentityUserClaim(claim));
            return Task.FromResult(0);
        }

        public Task<IList<Claim>> GetClaimsAsync(TUser user)
        {
            Guard.ArgumentNotNull(user, "user");

            return Task.FromResult<IList<Claim>>(user.Claims.Select(x => new Claim(x.ClaimType, x.ClaimValue)).ToList());
        }

        public Task RemoveClaimAsync(TUser user, Claim claim)
        {
            Guard.ArgumentNotNull(user, "user");
            Guard.ArgumentNotNull(claim, "claim");

            IdentityUserClaim userClaim = user.Claims.FirstOrDefault(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value);
            if (userClaim != null)
            {
                user.Claims.Remove(userClaim);
            }

            return Task.FromResult(0);
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                // Free managed
            }
            _disposed = true;
        }
        #endregion

        private CloudTableClient GetTableClient(string storageConnectionString)
        {
            Guard.ArgumentNotNullOrWhiteSpace(storageConnectionString, "storageConnectionString");
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            return storageAccount.CreateCloudTableClient();
        }
        private CloudTable GetUserTable()
        {
            return _userTableReference;
        }
        private CloudTable GetLoginTable()
        {
            return _loginTableReference;
        }
        private async Task UpdateUser(TUser user)
        {
            var operation = TableOperation.Replace(user);
            await GetUserTable().ExecuteAsync(operation);
        }

        #region IUserEmailStore
        public Task<TUser> FindByEmailAsync(string email)
        {
            var operation = TableOperation.Retrieve<TUser>(
            var result = await GetLoginTable().ExecuteAsync(operation);
            var loginInfoEntity = (IdentityUserLogin)result.Result;
            if (loginInfoEntity == null)
                return null;
            return await FindByIdAsync(loginInfoEntity.UserId);
        }

        public Task<string> GetEmailAsync(TUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetEmailConfirmedAsync(TUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailAsync(TUser user, string email)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(TUser user, bool confirmed)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

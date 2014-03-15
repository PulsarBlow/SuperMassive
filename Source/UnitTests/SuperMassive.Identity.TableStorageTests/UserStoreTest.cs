using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.Storage;
using SuperMassive.Identity.TableStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperMassive.Identity.TableStorageTests
{
    [TestClass]
    public class UserStoreTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task CreateAsync_WithNullUser_ShouldFail()
        {
            UserStore<IdentityUser> store = GetUserStore();
            await store.CreateAsync(null);
        }

        [TestMethod]
        public async Task CreateAsync_WithSuccess()
        {
            UserStore<IdentityUser> store = GetUserStore();
            IdentityUser user = CreateRandomUser();
            await store.CreateAsync(user);
        }

        [TestMethod]
        public async Task FindByIdAsync_WithSuccess()
        {
            IdentityUser expected = await CreateAndPersistRandomUserAsync();

            UserStore<IdentityUser> store = GetUserStore();
            IdentityUser actual = await store.FindByIdAsync(expected.Id);

            AreEquals(expected, actual);
        }

        [TestMethod]
        public async Task FindByNameAsync_WithSuccess()
        {
            IdentityUser expected = await CreateAndPersistRandomUserAsync();

            UserStore<IdentityUser> store = GetUserStore();
            IdentityUser actual = await store.FindByNameAsync(expected.UserName);

            AreEquals(expected, actual);
        }

        [TestMethod]
        public async Task DeleteAsync_WithSuccess()
        {
            UserStore<IdentityUser> store = GetUserStore();
            IdentityUser expected = CreateRandomUser();
            await store.CreateAsync(expected);
            await store.DeleteAsync(expected);
            // Check if user is deleted from store
            IdentityUser actual = await store.FindByIdAsync(expected.Id);
            Assert.IsNull(actual);
        }

        [TestMethod]
        public async Task UpdateAsync_WithSuccess()
        {
            IdentityUser user = await CreateAndPersistRandomUserAsync();

            UserStore<IdentityUser> store = GetUserStore();

            IdentityUserClaim newClaim = CreateRandomUserClaim();
            user.Claims.Add(newClaim);
            await store.UpdateAsync(user);

            IdentityUser actual = await store.FindByIdAsync(user.Id);
            IdentityUserClaim updatedClaim = actual.Claims.FirstOrDefault(x => x.ClaimType == newClaim.ClaimType);
            Assert.IsNotNull(updatedClaim);
            AreEquals(newClaim, updatedClaim);
        }

        [TestMethod]
        public async Task AddLoginAsync_WithSuccess()
        {
            IdentityUser user = await CreateAndPersistRandomUserAsync();
            UserStore<IdentityUser> store = GetUserStore();

            UserLoginInfo newLoginInfo = CreateRandomUserLoginInfo();
            await store.AddLoginAsync(user, newLoginInfo);
        }

        [TestMethod]
        public async Task FindAsync_WithSuccess()
        {
            IdentityUser expected = await CreateAndPersistRandomUserAsync();
            UserStore<IdentityUser> store = GetUserStore();

            UserLoginInfo newLoginInfo = CreateRandomUserLoginInfo();
            await store.AddLoginAsync(expected, newLoginInfo);

            IdentityUser actual = await store.FindAsync(newLoginInfo);

            Assert.IsNotNull(actual);
            AreEquals(expected, actual);
        }

        [TestMethod]
        public async Task GetLoginAsync_WithSuccess()
        {
            IdentityUser user = await CreateAndPersistRandomUserAsync();
            UserStore<IdentityUser> store = GetUserStore();

            UserLoginInfo newLoginInfo = CreateRandomUserLoginInfo();
            await store.AddLoginAsync(user, newLoginInfo);

            IList<UserLoginInfo> logins = await store.GetLoginsAsync(user);
            Assert.IsNotNull(logins);
            Assert.IsNotNull(logins.FirstOrDefault(x => x.LoginProvider == newLoginInfo.LoginProvider && x.ProviderKey == newLoginInfo.ProviderKey));
        }

        [TestMethod]
        public async Task RemoveLoginAsync_WithSuccess()
        {
            IdentityUser user = await CreateAndPersistRandomUserAsync();
            UserStore<IdentityUser> store = GetUserStore();

            UserLoginInfo newLoginInfo = CreateRandomUserLoginInfo();
            await store.AddLoginAsync(user, newLoginInfo);
            await store.RemoveLoginAsync(user, newLoginInfo);

            IdentityUser actual = await store.FindAsync(newLoginInfo);

            Assert.IsNull(actual);
        }

        [TestMethod]
        public async Task AddToRoleAsync_WithSuccess()
        {
            IdentityUser user = await CreateAndPersistRandomUserAsync();
            UserStore<IdentityUser> store = GetUserStore();

            await store.AddToRoleAsync(user, Guid.NewGuid().ToString());
        }

        [TestMethod]
        public async Task GetRolesAsync_WithSuccess()
        {
            IdentityUser user = await CreateAndPersistRandomUserAsync();
            UserStore<IdentityUser> store = GetUserStore();

            await store.AddToRoleAsync(user, Guid.NewGuid().ToString());

            IList<string> roles = await store.GetRolesAsync(user);
            CollectionAssert.AreEqual(user.Roles.ToArray(), roles.ToArray());
        }

        [TestMethod]
        public async Task IsInRoleAsync_WithSuccess()
        {
            IdentityUser user = await CreateAndPersistRandomUserAsync();
            UserStore<IdentityUser> store = GetUserStore();

            string newRoleName = Guid.NewGuid().ToString();
            await store.AddToRoleAsync(user, newRoleName);

            bool isInRole = await store.IsInRoleAsync(user, newRoleName);
            Assert.IsTrue(isInRole);
        }

        [TestMethod]
        public async Task RemoveFromRoleAsync_WithSuccess()
        {
            IdentityUser user = await CreateAndPersistRandomUserAsync();
            UserStore<IdentityUser> store = GetUserStore();

            string newRoleName = Guid.NewGuid().ToString();
            await store.AddToRoleAsync(user, newRoleName);
            await store.RemoveFromRoleAsync(user, newRoleName);

            IdentityUser actual = await store.FindByNameAsync(user.UserName);
            Assert.IsFalse(user.Roles.Contains(newRoleName));
        }

        [TestMethod]
        public async Task GetPasswordHashAsync_WithSuccess()
        {
            IdentityUser user = await CreateAndPersistRandomUserAsync();
            UserStore<IdentityUser> store = GetUserStore();

            string passwordHash = await store.GetPasswordHashAsync(user);
            Assert.AreEqual(user.PasswordHash, passwordHash);
        }

        [TestMethod]
        public async Task HasPasswordAsync_WithSuccess()
        {
            IdentityUser user = await CreateAndPersistRandomUserAsync();
            UserStore<IdentityUser> store = GetUserStore();

            bool hasPasswordHash = await store.HasPasswordAsync(user);
            Assert.IsFalse(hasPasswordHash);
            user.PasswordHash = CryptographyHelper.ComputeSHA256Hash(Guid.NewGuid().ToString());
            hasPasswordHash = await store.HasPasswordAsync(user);
            Assert.IsTrue(hasPasswordHash);
        }

        [TestMethod]
        public async Task SetPasswordHashAsync_WithSuccess()
        {
            IdentityUser user = await CreateAndPersistRandomUserAsync();
            UserStore<IdentityUser> store = GetUserStore();

            bool hasPasswordHash = await store.HasPasswordAsync(user);
            Assert.IsFalse(hasPasswordHash);

            string passwordHash = CryptographyHelper.ComputeSHA256Hash(Guid.NewGuid().ToString());
            await store.SetPasswordHashAsync(user, passwordHash);



        }
        #region Utilities
        static CloudStorageAccount GetStorageAccount()
        {
            return CloudStorageAccount.DevelopmentStorageAccount;
        }
        static UserPartitionKeyResolver GetPartitionKeyResolver()
        {
            return new UserPartitionKeyResolver(Guid.Empty.ToString());
        }
        static UserStore<IdentityUser> GetUserStore()
        {
            return new UserStore<IdentityUser>(GetStorageAccount(), GetPartitionKeyResolver());
        }
        static IdentityUser CreateRandomUser()
        {
            string userName = Guid.NewGuid().ToString();

            return new IdentityUser
            {
                UserName = userName,
                Claims = new List<IdentityUserClaim> { CreateRandomUserClaim() },
                Logins = new List<IdentityUserLogin> { new IdentityUserLogin(userName, CreateRandomUserLoginInfo()) },
                Roles = new List<string> { "Administrator", "User" }
            };
        }
        static UserLoginInfo CreateRandomUserLoginInfo()
        {
            return new UserLoginInfo(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
        }
        static IdentityUserClaim CreateRandomUserClaim()
        {
            return new IdentityUserClaim(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
        }
        static async Task<IdentityUser> CreateAndPersistRandomUserAsync()
        {
            UserStore<IdentityUser> store = GetUserStore();
            IdentityUser user = CreateRandomUser();
            await store.CreateAsync(user);
            return user;
        }

        static void AreEquals(IdentityUser expected, IdentityUser actual)
        {
            CheckNullReferences(expected, actual);
            if (expected == null) return;

            Assert.AreEqual(expected.ETag, actual.ETag);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.PartitionKey, actual.PartitionKey);
            Assert.AreEqual(expected.PasswordHash, actual.PasswordHash);
            Assert.AreEqual(expected.RowKey, actual.RowKey);
            Assert.AreEqual(expected.SecurityStamp, actual.SecurityStamp);
            Assert.AreEqual(expected.SerializedClaims, actual.SerializedClaims);
            Assert.AreEqual(expected.SerializedLogins, actual.SerializedLogins);
            Assert.AreEqual(expected.SerializedRoles, actual.SerializedRoles);
            AreSimilar(expected.Timestamp, actual.Timestamp);
            Assert.AreEqual(expected.UserName, actual.UserName);
            AreCollectionEquals(expected.Logins, actual.Logins, AreEquals);
            AreCollectionEquals(expected.Roles, actual.Roles, Assert.AreEqual);
            AreCollectionEquals(expected.Claims, actual.Claims, AreEquals);
        }

        static void AreEquals(IdentityUserLogin expected, IdentityUserLogin actual)
        {
            CheckNullReferences(expected, actual);
            if (expected == null) return;

            Assert.AreEqual(expected.ETag, actual.ETag);
            Assert.AreEqual(expected.PartitionKey, actual.PartitionKey);
            Assert.AreEqual(expected.RowKey, actual.RowKey);
            AreSimilar(expected.Timestamp, actual.Timestamp);
            Assert.AreEqual(expected.UserId, actual.UserId);
            Assert.AreEqual(expected.LoginProvider, actual.LoginProvider);
            Assert.AreEqual(expected.ProviderKey, actual.ProviderKey);
        }

        static void AreEquals(IList<IdentityUserLogin> expected, IList<IdentityUserLogin> actual)
        {
            AreCollectionEquals(expected, actual, AreEquals);
        }

        static void AreEquals(UserLoginInfo expected, UserLoginInfo actual)
        {
            CheckNullReferences(expected, actual);
            if (expected == null) return;
            Assert.AreEqual(expected.LoginProvider, actual.LoginProvider);
            Assert.AreEqual(expected.ProviderKey, actual.ProviderKey);
        }
        static void AreEquals(IList<UserLoginInfo> expected, IList<UserLoginInfo> actual)
        {
            AreCollectionEquals(expected, actual, AreEquals);
        }

        static void AreEquals(IdentityUserClaim expected, IdentityUserClaim actual)
        {
            CheckNullReferences(expected, actual);

            Assert.AreEqual(expected.ClaimType, actual.ClaimType);
            Assert.AreEqual(expected.ClaimValue, actual.ClaimValue);
        }

        static void AreEquals(IList<IdentityUserClaim> expected, IList<IdentityUserClaim> actual)
        {
            AreCollectionEquals(expected, actual, AreEquals);
        }

        static void AreSimilar(DateTimeOffset expected, DateTimeOffset actual)
        {
            if (expected == DateTimeOffset.MinValue && actual != DateTimeOffset.MinValue)
                Assert.Fail("Expected is not set while actual has a value");
            if (expected != DateTimeOffset.MinValue && actual == DateTimeOffset.MinValue)
                Assert.Fail("Expected has a value while actual is not set");
            string dateFormat = "yyyy/MM/dd HH:mm:ss";
            Assert.AreEqual(expected.ToString(dateFormat), actual.ToString(dateFormat));
        }
        static void AreCollectionEquals<T>(IEnumerable<T> expected, IEnumerable<T> actual, Action<T, T> comparer)
        {
            CheckNullReferences(expected, actual);
            if (expected == null)
                return;
            Assert.AreEqual(expected.Count(), actual.Count());
            for (int i = 0; i < expected.Count(); i++)
            {
                comparer(expected.ElementAt(i), actual.ElementAt(i));
            }
        }

        static void CheckNullReferences<T>(T expected, T actual) where T : class
        {
            if (expected == null && actual != null)
                Assert.Fail("Expected is null, actual is not null");
            if (expected != null && actual == null)
                Assert.Fail("Expected is not null, actual is null");
        }
        #endregion
    }
}

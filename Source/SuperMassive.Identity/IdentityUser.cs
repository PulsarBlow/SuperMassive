using Microsoft.AspNet.Identity;

namespace SuperMassive.Identity
{
    public abstract class IdentityUser : IUser
    {
        public string Id { get; set; }

        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;

namespace WebStore.Domain.Entities.Identity
{
    public class User : IdentityUser
    {
        public const string Administrator = "admin";

        public const string DefaultAdminPassword = "admin";
    }
}

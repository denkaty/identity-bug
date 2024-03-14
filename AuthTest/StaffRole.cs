using Microsoft.AspNetCore.Identity;

namespace AuthTest
{
    public class StaffRole : IdentityRole
    {
        public StaffRole() : base() { }

        public StaffRole(string roleName) : base(roleName)
        {
        }
    }
}
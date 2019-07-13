using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace AuthToken.DataAccess.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
        {

        }

        public ApplicationRole(string roleName) : base(roleName)
        {

        }

        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}

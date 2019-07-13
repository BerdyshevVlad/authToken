using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace AuthToken.DataAccess.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }

        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}

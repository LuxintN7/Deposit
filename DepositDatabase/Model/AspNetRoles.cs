using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DepositDatabase.Model
{
    public class AspNetRoles : IdentityRole
    {
        public AspNetRoles()
        {
            AspNetUsers = new HashSet<AspNetUser>();
        }

        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
    }
}

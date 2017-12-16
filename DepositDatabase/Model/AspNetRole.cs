using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DepositDatabase.Model
{
    public class AspNetRole : IdentityRole
    {
        public AspNetRole()
        {
            AspNetUsers = new HashSet<AspNetUser>();
        }

        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
    }
}

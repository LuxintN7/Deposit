using Microsoft.AspNet.Identity.EntityFramework;

namespace DepositDatabase.Model
{
    public class AspNetUserLogins : IdentityUserLogin
    {
        public virtual AspNetUser AspNetUsers { get; set; }
    }
}

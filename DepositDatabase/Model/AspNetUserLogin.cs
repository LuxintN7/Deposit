using Microsoft.AspNet.Identity.EntityFramework;

namespace DepositDatabase.Model
{
    public class AspNetUserLogin : IdentityUserLogin
    {
        public virtual AspNetUser AspNetUser { get; set; }
    }
}

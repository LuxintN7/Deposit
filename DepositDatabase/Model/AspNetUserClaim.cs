using Microsoft.AspNet.Identity.EntityFramework;

namespace DepositDatabase.Model
{
    public class AspNetUserClaim : IdentityUserClaim
    {
        public virtual AspNetUser AspNetUser { get; set; }
    }
}

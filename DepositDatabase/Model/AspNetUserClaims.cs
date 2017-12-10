using Microsoft.AspNet.Identity.EntityFramework;

namespace DepositDatabase.Model
{
    public class AspNetUserClaims : IdentityUserClaim
    {
        public virtual AspNetUser AspNetUsers { get; set; }
    }
}

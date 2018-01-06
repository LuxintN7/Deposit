using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DepositDatabaseCore.Model
{
    public class AspNetUser : IdentityUser
    {
        public AspNetUser()
        {
            Cards = new HashSet<Card>();
            Deposits = new HashSet<Deposit>();
            //AspNetRoles = new HashSet<AspNetRole>();
        }

        public virtual ICollection<Card> Cards { get; set; }
        public virtual ICollection<Deposit> Deposits { get; set; }
        //public virtual ICollection<AspNetRole> AspNetRoles { get; set; }
    }
}

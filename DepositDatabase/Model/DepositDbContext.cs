using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DepositDatabase.Model
{
    public class DepositDbContext : IdentityDbContext<AspNetUser>
    {      
        public DepositDbContext()
            : base("name=DepositDbContext", throwIfV1Schema:false)
        {
        }

        public virtual DbSet<CardHistory> CardHistory { get; set; }
        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<Deposit> Deposits { get; set; }
        public virtual DbSet<DepositState> DepositStates { get; set; }
        public virtual DbSet<DepositTerm> DepositTerms { get; set; }
        public virtual DbSet<DepositWayOfAccumulation> DepositWaysOfAccumulation { get; set; }        
    }
}
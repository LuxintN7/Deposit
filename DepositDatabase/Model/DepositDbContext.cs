using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DepositDatabase.Model
{
    public class DepositDbContext : IdentityDbContext<AspNetUser>
    {
        private readonly IDepositDbContextFactory factory;
        
        public DepositDbContext()
            : base("name=DepositDbContext", throwIfV1Schema:false)
        {
        }

        public virtual DbSet<CardHistory> CardHistory { get; set; }
        public virtual DbSet<Cards> Cards { get; set; }
        public virtual DbSet<Currencies> Currencies { get; set; }
        public virtual DbSet<Deposits> Deposits { get; set; }
        public virtual DbSet<DepositStates> DepositStates { get; set; }
        public virtual DbSet<DepositTerms> DepositTerms { get; set; }
        public virtual DbSet<DepositWaysOfAccumulation> DepositWaysOfAccumulation { get; set; }        
    }
}
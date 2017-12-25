
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DepositDatabaseCore.Model
{
    public class DepositDbContext : IdentityDbContext<AspNetUser>
    {
        public DepositDbContext(DbContextOptions<DepositDbContext> options)
            : base(options)
        {
        }

        public DepositDbContext()
        {
        }

        public virtual DbSet<CardHistory> CardHistory { get; set; }
        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<Deposit> Deposits { get; set; }
        public virtual DbSet<DepositState> DepositStates { get; set; }
        public virtual DbSet<DepositTerm> DepositTerms { get; set; }
        public virtual DbSet<DepositWayOfAccumulation> DepositWaysOfAccumulation { get; set; }

        public override void Dispose()
        {
            if (this.HasUnsavedChanges()) this.SaveChanges();

            base.Dispose();
        }
    }
}
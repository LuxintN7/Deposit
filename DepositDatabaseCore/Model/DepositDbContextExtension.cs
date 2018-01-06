using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DepositDatabaseCore.Model
{
    public static class DepositDbContextExtension
    {
        public static bool HasUnsavedChanges(this DepositDbContext dbContext)
        {
            return dbContext.ChangeTracker.Entries().Any(e => e.State == EntityState.Added
                                           || e.State == EntityState.Modified
                                           || e.State == EntityState.Deleted);
        }
    }
}

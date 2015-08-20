using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepositDatabase.Model
{
    public static class DbContextExtension
    {
        public static bool HasUnsavedChanges(this DbContext dbContext)
        {
            return dbContext.ChangeTracker.Entries().Any(e => e.State == EntityState.Added
                                           || e.State == EntityState.Modified
                                           || e.State == EntityState.Deleted);
        }
    }
}

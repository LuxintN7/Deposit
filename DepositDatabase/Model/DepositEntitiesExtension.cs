using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DepositDatabase.Model;

namespace DepositDatabase.Model
{
    public static class DepositEntitiesExtension
    {
        public static bool HasUnsavedChanges(this DepositEntities dbContext)
        {
            return dbContext.ChangeTracker.Entries().Any(e => e.State == EntityState.Added
                                           || e.State == EntityState.Modified
                                           || e.State == EntityState.Deleted);
        }
        
        public static DepositEntities GetInstance()
        {
            var instance = (DepositEntities) HttpContext.Current.Items[typeof (DepositEntities)];
            if (instance == null)
            {
                instance = new DepositEntities();
                Debug.WriteLine("   INSTANCIATED " + instance.GetHashCode());
                HttpContext.Current.Items[typeof (DepositEntities)] = instance;
                HttpContext.Current.AddOnRequestCompleted(OnRequestCompleted);
            }
            return instance;
        }

        private static void OnRequestCompleted(HttpContext context)
        {
            var instance = (DepositEntities)HttpContext.Current.Items[typeof(DepositEntities)];
            if (instance != null)
            {
                Debug.WriteLine("   DISPOSING... " + instance.GetHashCode());
                if (instance.HasUnsavedChanges()) instance.SaveChanges();
                instance.Dispose();
                Debug.WriteLine("   DISPOSED " + instance.GetHashCode());
            }
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using DepositDatabaseCore.Model;

namespace DepositDatabaseCore
{
    public class DepositTermsData
    {
        public static DepositTerm GetTermById(int id)
        {
            using (var dbContext = new DepositDbContext())
            {
                return GetTermById(dbContext, id);
            }
        }

        public static DepositTerm GetTermById(DepositDbContext dbContext, int id)
        {
            return dbContext.DepositTerms.First(d => d.Id == id);
        }

        public static List<DepositTerm> GetList()
        {
            using (var dbContext = new DepositDbContext())
            {
                return GetList(dbContext);
            }
        }

        public static List<DepositTerm> GetList(DepositDbContext dbContext)
        {
            return dbContext.DepositTerms.ToList();
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using DepositDatabaseCore.Model;

namespace DepositDatabaseCore
{
    public class CurrenciesData
    {
        public static Currency GetCurrencyById(int id)
        {
            using (var dbContext = new DepositDbContext())
            {
                return GetCurrencyById(dbContext, id);
            }
        }

        public static Currency GetCurrencyById(DepositDbContext dbContext, int id)
        {
            return dbContext.Currencies.Find(id);
        }


        public static List<Currency> GetList()
        {
            using (var dbContext = new DepositDbContext())
            {
                return GetList(dbContext);
            }
        }

        public static List<Currency> GetList(DepositDbContext dbContext)
        {
            return dbContext.Currencies.ToList();
        }
    }
}

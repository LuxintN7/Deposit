using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepositDatabase.Model;


namespace DepositDatabase
{
    public class CurrenciesData
    {
        public static Currencies GetCurrencyById(byte id)
        {
            using (var dbContext = new DepositEntities())
            {
                return GetCurrencyById(id, dbContext);
            }
        }

        public static Currencies GetCurrencyById(byte id, DepositEntities dbContext)
        {
            return dbContext.Currencies.Find(id);
        }

        public static List<Currencies> GetList()
        {
            using (var db = new DepositEntities())
            {
                return db.Currencies.ToList();
            }
        }
    }
}

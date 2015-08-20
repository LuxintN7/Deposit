using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepositDatabase.Model;


namespace DepositDatabase
{
    public class CurrenciesData : DomainLogic.Model.ICurrenciesService
    {
        public DomainLogic.Model.Currencies GetById(byte id)
        {
            return GetCurrencyById(id).ToDomainLogic();
        }

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
    }
}

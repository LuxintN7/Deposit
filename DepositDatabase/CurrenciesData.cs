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
            return DepositEntitiesExtension.GetInstance().Currencies.Find(id);
        }

        public static List<Currencies> GetList()
        {
            return DepositEntitiesExtension.GetInstance().Currencies.ToList();
        }
    }
}

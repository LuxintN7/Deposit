using System.Collections.Generic;
using System.Linq;
using DepositDatabase.Model;


namespace DepositDatabase
{
    public class CurrenciesData
    {
        public static Currency GetCurrencyById(byte id)
        {
            return DepositDbContextExtension.GetInstance().Currencies.Find(id);
        }

        public static List<Currency> GetList()
        {
            return DepositDbContextExtension.GetInstance().Currencies.ToList();
        }
    }
}

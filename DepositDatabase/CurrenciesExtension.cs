using System.Collections.Generic;
using DepositDatabase.Model;

namespace DepositDatabase
{
    public static class CurrenciesExtension
    {
        public static DomainLogic.Model.Currencies ToDomainLogic(this Currencies currency)
        {
            return new DomainLogic.Model.Currencies()
            {
                Id = currency.Id,
                Abbreviation = currency.Abbreviation,
                Name = currency.Name
            };
        }

        public static List<DomainLogic.Model.Currencies> ToDomainLogic(this List<Currencies> list)
        {
            var currencies = new List<DomainLogic.Model.Currencies>();

            foreach (var currency in list)
            {
                currencies.Add(currency.ToDomainLogic());
            }

            return currencies;
        }
    }
}
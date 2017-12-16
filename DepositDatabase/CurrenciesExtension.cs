using System.Collections.Generic;
using DepositDatabase.Model;

namespace DepositDatabase
{
    public static class CurrenciesExtension
    {
        public static DomainLogic.Model.Currency ToDomainLogic(this Currency currency)
        {
            return new DomainLogic.Model.Currency()
            {
                Id = currency.Id,
                Abbreviation = currency.Abbreviation,
                Name = currency.Name
            };
        }

        public static List<DomainLogic.Model.Currency> ToDomainLogic(this List<Currency> list)
        {
            var currencies = new List<DomainLogic.Model.Currency>();

            foreach (var currency in list)
            {
                currencies.Add(currency.ToDomainLogic());
            }

            return currencies;
        }
    }
}
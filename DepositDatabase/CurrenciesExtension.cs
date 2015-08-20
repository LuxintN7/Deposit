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
    }
}
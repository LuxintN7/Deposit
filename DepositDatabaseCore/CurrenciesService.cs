using System.Collections.Generic;

namespace DepositDatabaseCore
{
    public class CurrenciesService : DomainLogic.ICurrenciesService
    {
        public DomainLogic.Model.Currency GetById(byte id)
        {
            return CurrenciesData.GetCurrencyById(id).ToDomainLogic();
        }

        public List<DomainLogic.Model.Currency> GetList()
        {
            return CurrenciesData.GetList().ToDomainLogic();
        }
    }
}

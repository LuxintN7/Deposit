using System.Collections.Generic;

namespace DepositDatabase
{
    public class CurrenciesService : DomainLogic.ICurrenciesService
    {
        public DomainLogic.Model.Currency GetById(int id)
        {
            return CurrenciesData.GetCurrencyById(id).ToDomainLogic();
        }

        public List<DomainLogic.Model.Currency> GetList()
        {
            return CurrenciesData.GetList().ToDomainLogic();
        }
    }
}

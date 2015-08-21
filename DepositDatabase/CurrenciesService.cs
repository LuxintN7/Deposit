using System.Collections.Generic;

namespace DepositDatabase
{
    public class CurrenciesService : DomainLogic.ICurrenciesService
    {
        public DomainLogic.Model.Currencies GetById(byte id)
        {
            return CurrenciesData.GetCurrencyById(id).ToDomainLogic();
        }

        public List<DomainLogic.Model.Currencies> GetList()
        {
            return CurrenciesData.GetList().ToDomainLogic();
        }
    }
}

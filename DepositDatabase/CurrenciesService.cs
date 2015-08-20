using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepositDatabase.Model;

namespace DepositDatabase
{
    public class CurrenciesService : DomainLogic.Model.ICurrenciesService
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

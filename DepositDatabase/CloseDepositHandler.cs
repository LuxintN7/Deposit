using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepositDatabase
{
    public class CloseDepositHandler : DomainLogic.ICloseDepositHandler
    {
        public DomainLogic.Model.Cards GetCardById(string id)
        {
            throw new NotImplementedException();
        }

        public DomainLogic.Model.Currencies GetCurrencyByDepositTermsId(byte depositTermsId)
        {
            throw new NotImplementedException();
        }

        public DomainLogic.Model.Deposits GetDepositById(int id)
        {
            throw new NotImplementedException();
        }

        public void IncreaseCardBalanceByDepositBalance(decimal depositBalance, string cardId)
        {
            throw new NotImplementedException();
        }

        public void SetDepositState(string name, int depositId)
        {
            throw new NotImplementedException();
        }

        public void AddCardHistoryToDbContext(string cardId, string cardHistoryDescription)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

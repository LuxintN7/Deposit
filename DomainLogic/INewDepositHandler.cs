using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLogic.Model;

namespace DomainLogic
{
    public interface INewDepositHandler : IDisposable
    {
        Cards GetCardById(string id);
        Currencies GetCurrencyByDepositTermsId(byte depositTermsId);
        Deposits CreateNewDeposit(decimal depositAmount, byte wayOfAccumulationId, string userId, byte termsId, string cardId);
        void DecreaseCardBalanceByDepositAmount(decimal depositAmount, string cardId);
        void AddCardHistoryToDbContext(string cardId, string cardHistoryDescription);
    }
}

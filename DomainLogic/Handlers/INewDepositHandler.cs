using System;
using DomainLogic.Model;

namespace DomainLogic.Handlers
{
    public interface INewDepositHandler
    {
        Cards GetCardById(string id);
        Currencies GetCurrencyByDepositTermsId(byte depositTermsId);
        Deposits CreateNewDeposit(decimal depositAmount, byte wayOfAccumulationId, string userId, byte termsId, string cardId);
        void DecreaseCardBalanceByDepositAmount(decimal depositAmount, string cardId);
        void AddCardHistoryRecord(string cardId, string cardHistoryDescription);
    }
}

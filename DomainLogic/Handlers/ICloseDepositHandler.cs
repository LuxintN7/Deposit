using System;
using DomainLogic.Model;

namespace DomainLogic.Handlers
{
    public interface ICloseDepositHandler
    {
        Currencies GetCurrencyByDepositTermsId(byte depositTermsId);
        Deposits GetDepositById(int id);
        void IncreaseCardBalanceByDepositBalance(decimal depositBalance, string cardId);
        void SetDepositState(string name, int depositId);
        void AddCardHistoryRecord(string cardId, string cardHistoryDescription);
    }
}
using System;
using DomainLogic.Model;

namespace DomainLogic
{
    public interface ICloseDepositHandler : IDisposable
    {
        Currencies GetCurrencyByDepositTermsId(byte depositTermsId);
        Deposits GetDepositById(int id);
        void IncreaseCardBalanceByDepositBalance(decimal depositBalance, string cardId);
        void SetDepositState(string name, int depositId);
        void AddCardHistoryRecord(string cardId, string cardHistoryDescription);
    }
}
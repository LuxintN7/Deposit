using System;
using DomainLogic.Model;

namespace DomainLogic
{
    public interface ICloseDepositHandler : IDisposable
    {
        Cards GetCardById(string id);
        Currencies GetCurrencyByDepositTermsId(byte depositTermsId);
        Deposits GetDepositById(int id);
        void IncreaseCardBalanceByDepositBalance(decimal depositBalance, string cardId);
        void SetDepositState(string name, int depositId);
        void AddCardHistoryToDbContext(string cardId, string cardHistoryDescription);
    }
}
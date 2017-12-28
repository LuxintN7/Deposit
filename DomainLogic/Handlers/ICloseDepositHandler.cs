using DomainLogic.Model;

namespace DomainLogic.Handlers
{
    public interface ICloseDepositHandler
    {
        Currency GetCurrencyByDepositTermsId(int depositTermsId);
        Deposit GetDepositById(int id);
        void IncreaseCardBalanceByDepositBalance(decimal depositBalance, string cardId);
        void SetDepositState(string name, int depositId);
        void AddCardHistoryRecord(string cardId, string cardHistoryDescription);
    }
}
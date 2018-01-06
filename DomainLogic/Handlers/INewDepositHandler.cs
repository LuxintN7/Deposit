using DomainLogic.Model;

namespace DomainLogic.Handlers
{
    public interface INewDepositHandler
    {
        Card GetCardById(string id);
        Currency GetCurrencyByDepositTermsId(int depositTermsId);
        Deposit CreateNewDeposit(decimal depositAmount, int wayOfAccumulationId, string userId, int termsId, string cardId);
        void DecreaseCardBalanceByDepositAmount(decimal depositAmount, string cardId);
        void AddCardHistoryRecord(string cardId, string cardHistoryDescription);
    }
}

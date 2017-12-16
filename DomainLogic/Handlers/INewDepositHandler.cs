using DomainLogic.Model;

namespace DomainLogic.Handlers
{
    public interface INewDepositHandler
    {
        Card GetCardById(string id);
        Currency GetCurrencyByDepositTermsId(byte depositTermsId);
        Deposit CreateNewDeposit(decimal depositAmount, byte wayOfAccumulationId, string userId, byte termsId, string cardId);
        void DecreaseCardBalanceByDepositAmount(decimal depositAmount, string cardId);
        void AddCardHistoryRecord(string cardId, string cardHistoryDescription);
    }
}

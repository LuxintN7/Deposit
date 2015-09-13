using DepositDatabase.Model;

namespace DepositDatabase.Handlers
{
    public class CloseDepositHandler : DomainLogic.Handlers.ICloseDepositHandler
    {
        public DomainLogic.Model.Currencies GetCurrencyByDepositTermsId(byte depositTermsId)
        {
            return DepositTermsData.GetTermsById(depositTermsId).Currencies.ToDomainLogic();
        }

        public DomainLogic.Model.Deposits GetDepositById(int id)
        {
            return DepositsData.GetDepositById(id).ToDomainLogic();
        }

        public void IncreaseCardBalanceByDepositBalance(decimal depositBalance, string cardId)
        {
            var card = CardsData.GetCardById(cardId);
            card.Balance += depositBalance;
        }

        public void SetDepositState(string name, int depositId)
        {
            var deposit = DepositsData.GetDepositById(depositId);
            deposit.DepositStates = DepositStatesData.GetStateByName(name);
        }

        public void AddCardHistoryRecord(string cardId, string cardHistoryDescription)
        {
            var card = CardsData.GetCardById(cardId);
            CardHistoryData.AddRecordToDbContext(card, cardHistoryDescription);
        }
    }
}

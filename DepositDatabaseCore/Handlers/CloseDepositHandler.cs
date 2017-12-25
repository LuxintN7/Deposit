namespace DepositDatabaseCore.Handlers
{
    public class CloseDepositHandler : DomainLogic.Handlers.ICloseDepositHandler
    {
        public DomainLogic.Model.Currency GetCurrencyByDepositTermsId(byte depositTermsId)
        {
            return DepositTermsData.GetTermById(depositTermsId).Currency.ToDomainLogic();
        }

        public DomainLogic.Model.Deposit GetDepositById(int id)
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
            deposit.DepositState = DepositStatesData.GetStateByName(name);
        }

        public void AddCardHistoryRecord(string cardId, string cardHistoryDescription)
        {
            var card = CardsData.GetCardById(cardId);
            CardHistoryData.AddRecordToDbContext(card, cardHistoryDescription);
        }
    }
}

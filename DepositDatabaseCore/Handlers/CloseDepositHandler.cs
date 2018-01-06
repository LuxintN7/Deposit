using DepositDatabaseCore.Model;
using Currency = DomainLogic.Model.Currency;

namespace DepositDatabaseCore.Handlers
{
    public class CloseDepositHandler : DomainLogic.Handlers.ICloseDepositHandler
    {
        public Currency GetCurrencyByDepositTermsId(int depositTermsId)
        {
            return DepositTermsData.GetTermById(depositTermsId).Currency.ToDomainLogic();
        }

        public DomainLogic.Model.Deposit GetDepositById(int id)
        {
            return DepositsData.GetDepositById(id).ToDomainLogic();
        }

        public void IncreaseCardBalanceByDepositBalance(decimal depositBalance, string cardId)
        {
            using (var dbContext = new DepositDbContext())
            {
                var card = CardsData.GetCardById(dbContext, cardId);
                card.Balance += depositBalance;
            }
        }

        public void SetDepositState(string name, int depositId)
        {
            using (var dbContext = new DepositDbContext())
            {
                var deposit = DepositsData.GetDepositById(dbContext, depositId);
                deposit.DepositStateId = DepositStatesData.GetStateByName(name).Id;
            }
        }

        public void AddCardHistoryRecord(string cardId, string cardHistoryDescription)
        {
            using (var dbContext = new DepositDbContext())
            {
                var card = CardsData.GetCardById(dbContext, cardId);
                CardHistoryData.AddRecordToDbContext(dbContext, card, cardHistoryDescription);
            }
        }
    }
}

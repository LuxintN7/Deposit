using DepositDatabase.Model;

namespace DepositDatabase.Handlers
{
    public class CloseDepositHandler : DomainLogic.Handlers.ICloseDepositHandler
    {
        private readonly DepositEntities dbContext;

        public CloseDepositHandler()
        {
            dbContext = new DepositEntities();
        }

        public DomainLogic.Model.Currencies GetCurrencyByDepositTermsId(byte depositTermsId)
        {
            return DepositTermsData.GetTermsById(depositTermsId, dbContext).Currencies.ToDomainLogic();
        }

        public DomainLogic.Model.Deposits GetDepositById(int id)
        {
            return DepositsData.GetDepositById(id, dbContext).ToDomainLogic();
        }

        public void IncreaseCardBalanceByDepositBalance(decimal depositBalance, string cardId)
        {
            var card = CardsData.GetCardById(cardId, dbContext);
            card.Balance += depositBalance;
        }

        public void SetDepositState(string name, int depositId)
        {
            var deposit = DepositsData.GetDepositById(depositId, dbContext);
            deposit.DepositStates = DepositStatesData.GetStateByName(name, dbContext);
        }

        public void AddCardHistoryRecord(string cardId, string cardHistoryDescription)
        {
            var card = CardsData.GetCardById(cardId, dbContext);
            CardHistoryData.AddRecordToDbContext(card, cardHistoryDescription, dbContext);
        }

        public void Dispose()
        {
            if (dbContext != null)
            {
                if (dbContext.HasUnsavedChanges())
                {
                    dbContext.SaveChanges();
                }

                dbContext.Dispose();
            }
        }
    }
}

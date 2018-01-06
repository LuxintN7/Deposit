using System;
using DepositDatabaseCore.Model;
using Currency = DomainLogic.Model.Currency;
using DepositState = DomainLogic.Model.DepositState;

namespace DepositDatabaseCore.Handlers
{
    public class NewDepositHandler : DomainLogic.Handlers.INewDepositHandler
    {
        public DomainLogic.Model.Card GetCardById(string id)
        {
            return CardsData.GetCardById(id).ToDomainLogic();
        }

        public Currency GetCurrencyByDepositTermsId(int termsId)
        {
            return DepositTermsData.GetTermById(termsId).Currency.ToDomainLogic();
        }

        public DomainLogic.Model.Deposit CreateNewDeposit(decimal depositAmount, int wayOfAccumulationId, string userId, int termsId, string cardId)
        {
            var newDeposit = new Deposit()
            {
                UserOwnerId = userId,
                InitialAmount = depositAmount,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(DepositTermsData.GetTermById(termsId).Months),
                Balance = depositAmount,
                DepositWayOfAccumulationId = wayOfAccumulationId,
                CardId = cardId,
                DepositTermId = termsId,
                DepositStateId = DepositStatesData.GetStateByName(DepositState.OpenedDepositStateName).Id
            };

            DepositsData.AddNewDepositToDbContext(newDeposit);

            return newDeposit.ToDomainLogic();
        }

        public void DecreaseCardBalanceByDepositAmount(decimal depositAmount, string cardId)
        {
            using (var dbContext = new DepositDbContext())
            {
                var card = CardsData.GetCardById(dbContext, cardId);
                card.Balance -= depositAmount;
            }
        }

        public void AddCardHistoryRecord(string cardId, string cardHistoryDescription)
        {
            var card = CardsData.GetCardById(cardId);
            CardHistoryData.AddRecordToDbContext(card, cardHistoryDescription);
        }
    }
}

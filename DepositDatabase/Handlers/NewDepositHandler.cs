using System;
using DepositDatabase.Model;

namespace DepositDatabase.Handlers
{
    public class NewDepositHandler : DomainLogic.Handlers.INewDepositHandler
    {
        public DomainLogic.Model.Cards GetCardById(string id)
        {
            return CardsData.GetCardById(id).ToDomainLogic();
        }

        public DomainLogic.Model.Currencies GetCurrencyByDepositTermsId(byte termsId)
        {
            return DepositTermsData.GetTermsById(termsId).Currencies.ToDomainLogic();
        }

        public DomainLogic.Model.Deposits CreateNewDeposit(decimal depositAmount, byte wayOfAccumulationId, string userId, byte termsId, string cardId)
        {
            var depositTerms = DepositTermsData.GetTermsById(termsId);

            var newDeposit = new Deposits()
            {
                UserOwnerId = userId,
                InitialAmount = depositAmount,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(depositTerms.Months),
                Balance = depositAmount,
                DepositWaysOfAccumulation = DepositWaysOfAccumulationData.GetWayById(wayOfAccumulationId),
                Cards = CardsData.GetCardById(cardId),
                DepositTerms = depositTerms,
                DepositStates = DepositStatesData.GetStateByName("Opened")
            };

            DepositsData.AddNewDepositToDbContext(newDeposit);

            return newDeposit.ToDomainLogic();
        }

        public void DecreaseCardBalanceByDepositAmount(decimal depositAmount, string cardId)
        {
            var card = CardsData.GetCardById(cardId);
            card.Balance -= depositAmount;
        }

        public void AddCardHistoryRecord(string cardId, string cardHistoryDescription)
        {
            var card = CardsData.GetCardById(cardId);
            CardHistoryData.AddRecordToDbContext(card, cardHistoryDescription);
        }
    }
}

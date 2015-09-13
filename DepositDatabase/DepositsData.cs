using System;
using System.Linq;
using DepositDatabase.Model;

namespace DepositDatabase
{
    public interface INewDeposit
    {
        string CardId { get; set; }
        byte WayOfAccumulationId { get; set; }
        decimal Amount { get; set; }
    }

    public static class DepositsData
    {
        // Required for InterestPaymentService
        public static Deposits GetDepositById(int id, DepositEntities dbContext)
        {
             return dbContext.Deposits.First(d => d.Id == id);
        }

        public static Deposits GetDepositById(int id)
        {
            return DepositEntitiesExtension.GetInstance().Deposits.First(d => d.Id == id);
        }

        public static void AddNewDepositToDbContext(Deposits newDeposit)
        {
            DepositEntitiesExtension.GetInstance().Deposits.Add(newDeposit);
        }

        public static void AddNewDepositToDbContext(INewDeposit depositModel, string userId, byte termsId, string cardId)
        {
            var newDeposit = CreateDeposit(depositModel, userId, termsId, cardId);
            DepositEntitiesExtension.GetInstance().Deposits.Add(newDeposit);
        }

        public static Deposits CreateDeposit(INewDeposit depositModel, string userId, byte termsId, string cardId)
        {
            var dbContext = DepositEntitiesExtension.GetInstance();
            var depositTerms = DepositTermsData.GetTermsById(termsId);

            var newDeposit = new Deposits()
            {
                UserOwnerId = userId,
                InitialAmount = depositModel.Amount,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(depositTerms.Months),
                Balance = Convert.ToDecimal(depositModel.Amount),
                DepositWaysOfAccumulation = DepositWaysOfAccumulationData.GetWayById(depositModel.WayOfAccumulationId),
                Cards = CardsData.GetCardById(cardId),
                DepositTerms = depositTerms,
                DepositStates = DepositStatesData.GetStateByName("Opened")
            };

            return newDeposit;
        }

    }
}

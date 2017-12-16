using System;
using System.Linq;
using DepositDatabase.Model;
using DepositState = DomainLogic.Model.DepositState;

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
        public static Deposit GetDepositById(int id, DepositDbContext dbContext)
        {
             return dbContext.Deposits.First(d => d.Id == id);
        }

        public static Deposit GetDepositById(int id)
        {
            return DepositDbContextExtension.GetInstance().Deposits.First(d => d.Id == id);
        }

        public static void AddNewDepositToDbContext(Deposit newDeposit)
        {
            DepositDbContextExtension.GetInstance().Deposits.Add(newDeposit);
        }

        public static void AddNewDepositToDbContext(INewDeposit depositModel, string userId, byte termsId, string cardId)
        {
            var newDeposit = CreateDeposit(depositModel, userId, termsId, cardId);
            DepositDbContextExtension.GetInstance().Deposits.Add(newDeposit);
        }

        public static Deposit CreateDeposit(INewDeposit depositModel, string userId, byte termsId, string cardId)
        {
            var dbContext = DepositDbContextExtension.GetInstance();
            var depositTerms = DepositTermsData.GetTermById(termsId);

            var newDeposit = new Deposit()
            {
                UserOwnerId = userId,
                InitialAmount = depositModel.Amount,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(depositTerms.Months),
                Balance = Convert.ToDecimal(depositModel.Amount),
                DepositWayOfAccumulation = DepositWaysOfAccumulationData.GetWayById(depositModel.WayOfAccumulationId),
                Card = CardsData.GetCardById(cardId),
                DepositTerm = depositTerms,
                DepositState = DepositStatesData.GetStateByName(DepositState.OpenedDepositStateName)
            };

            return newDeposit;
        }

    }
}

using System;
using System.Linq;
using DepositDatabaseCore.Model;
using DepositState = DomainLogic.Model.DepositState;

namespace DepositDatabaseCore
{
    public interface INewDeposit
    {
        string CardId { get; set; }
        byte WayOfAccumulationId { get; set; }
        decimal Amount { get; set; }
    }

    public static class DepositsData
    {
        public static Deposit GetDepositById(int id)
        {
            using (var dbContext = new DepositDbContext())
            {
                return GetDepositById(dbContext, id);
            }
        }

        // Required for InterestPaymentService
        public static Deposit GetDepositById(DepositDbContext dbContext, int id)
        {
             return dbContext.Deposits.First(d => d.Id == id);
        }

        public static void AddNewDepositToDbContext(Deposit newDeposit)
        {
            using (var dbContext = new DepositDbContext())
            {
                AddNewDepositToDbContext(dbContext, newDeposit);
            }
        }

        public static void AddNewDepositToDbContext(DepositDbContext dbContext, Deposit newDeposit)
        {
            dbContext.Deposits.Add(newDeposit);
        }

        public static void AddNewDepositToDbContext(DepositDbContext dbContext, INewDeposit depositModel, string userId, byte termsId, string cardId)
        {
            var newDeposit = CreateDeposit(dbContext, depositModel, userId, termsId, cardId);
            dbContext.Deposits.Add(newDeposit);
        }

        public static Deposit CreateDeposit(DepositDbContext dbContext, INewDeposit depositModel, string userId, byte termsId, string cardId)
        {
            var depositTerms = DepositTermsData.GetTermById(dbContext, termsId);

            var newDeposit = new Deposit()
            {
                UserOwnerId = userId,
                InitialAmount = depositModel.Amount,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(depositTerms.Months),
                Balance = Convert.ToDecimal(depositModel.Amount),
                DepositWayOfAccumulation = DepositWaysOfAccumulationData.GetWayById(dbContext, depositModel.WayOfAccumulationId),
                Card = CardsData.GetCardById(dbContext, cardId),
                DepositTerm = depositTerms,
                DepositState = DepositStatesData.GetStateByName(dbContext, DepositState.OpenedDepositStateName)
            };

            return newDeposit;
        }

    }
}

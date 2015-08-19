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
        public static Deposits GetDepositById(int id)
        {
            using (var dbContext = new DepositEntities())
            {
                return GetDepositById(id, dbContext);
            }
        }

        public static Deposits GetDepositById(int id, DepositEntities dbContext)
        {
             return dbContext.Deposits.First(d => d.Id == id);
        }

        public static void AddNewDepositToDbContext(Deposits newDeposit, DepositEntities dbContext)
        {
            dbContext.Deposits.Add(newDeposit);
        }

        public static void AddNewDepositToDbContext(INewDeposit depositModel, string userId, byte termsId, string cardId, DepositEntities dbContext)
        {
            var newDeposit = CreateDeposit(depositModel, userId, termsId, cardId, dbContext);
            dbContext.Deposits.Add(newDeposit);
        }

        public static Deposits CreateDeposit(INewDeposit depositModel, string userId, byte termsId, string cardId, DepositEntities dbContext)
        {
            var depositTerms = DepositTermsData.GetTermsById(termsId, dbContext);

            var newDeposit = new Deposits()
            {
                UserOwnerId = userId,
                InitialAmount = depositModel.Amount,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(depositTerms.Months),
                Balance = Convert.ToDecimal(depositModel.Amount),
                DepositWaysOfAccumulation = DepositWaysOfAccumulationData.GetWayById(depositModel.WayOfAccumulationId, dbContext),
                Cards = CardsData.GetCardById(cardId, dbContext),
                DepositTerms = depositTerms,
                DepositStates = DepositStatesData.GetStateByName("Opened", dbContext)
            };

            return newDeposit;
        }

    }
}

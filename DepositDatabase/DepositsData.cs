using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepositDatabase.Model;

namespace DepositDatabase
{
    public interface INewDeposit
    {
        string CardId { get; set; }
        int WayOfAccumulationId { get; set; }
        decimal Amount { get; set; }
    }

    public static class DepositsData
    {
        public static void AddNewDepositToDbContext(DepositEntities dbContext, Deposits newDeposit)
        {
            dbContext.Deposits.Add(newDeposit);
        }

        public static void AddNewDepositToDbContext(DepositEntities dbContext, INewDeposit depositModel, string userId, byte termsId, string cardId)
        {
            var newDeposit = CreateDeposit(dbContext, depositModel, userId, termsId, cardId);
            dbContext.Deposits.Add(newDeposit);
        }

        public static Deposits CreateDeposit(DepositEntities dbContext, INewDeposit depositModel, string userId, byte termsId, string cardId)
        {
            var depositTerms = dbContext.DepositTerms.First(dt => dt.Id == termsId);

            var newDeposit = new Deposits()
            {
                UserOwnerId = userId,
                InitialAmount = Convert.ToDecimal(depositModel.Amount),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(depositTerms.Months),
                Balance = Convert.ToDecimal(depositModel.Amount),
                DepositWaysOfAccumulation = dbContext.DepositWaysOfAccumulation.First(w => w.Id == depositModel.WayOfAccumulationId),
                Cards = dbContext.Cards.First(c => c.Id == cardId),
                DepositTerms = depositTerms,
                DepositStates = dbContext.DepositStates.First(ds => ds.Name == "Opened")
            };

            return newDeposit;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using DepositDatabase.Model;

namespace DepositDatabase.Handlers
{
    public class InterestPaymentHandler : DomainLogic.Handlers.IInterestPaymentHandler
    {
        private readonly DepositEntities dbContext;


        public InterestPaymentHandler()
        {
            dbContext = new DepositEntities();
        }

        public List<DomainLogic.Model.Deposits> GetActiveDeposits()
        {
            var deposits = (from d in dbContext.Deposits
                            where d.DepositStates.Name != "Closed"
                            select d).ToList();

            return deposits.ToDomainLogic();
        }
        
        public string GetDepositStateName(int depositId)
        {
            var deposit = DepositsData.GetDepositById(depositId, dbContext);
            return deposit.DepositStates.Name;
        }

        public void ExtendDeposit(int depositId, DateTime today)
        {
            var deposit = DepositsData.GetDepositById(depositId, dbContext);
            deposit.EndDate = today.AddMonths(deposit.DepositTerms.Months);
        }

        public void SetLastInterestPaymentDate(int depositId, DateTime date)
        {
            var deposit = DepositsData.GetDepositById(depositId, dbContext); 
            deposit.LastInterestPaymentDate = date;
        }

        public void SetDepositState(int depositId, string stateName)
        {
            var deposit = DepositsData.GetDepositById(depositId, dbContext);
            deposit.DepositStates = DepositStatesData.GetStateByName(stateName);
        }
        
        public void AddInterest(int depositId)
        {
            var deposit = DepositsData.GetDepositById(depositId, dbContext);

            const int daysInYear = 365;
            decimal interestRate = deposit.DepositTerms.InterestRate;
            DateTime today = DateTime.Today;

            byte daysCount = (byte)(today - (deposit.LastInterestPaymentDate ?? deposit.StartDate)).TotalDays;

            decimal interestSum = deposit.Balance * interestRate * daysCount / (100 * daysInYear);

            if (deposit.DepositWaysOfAccumulation.Name == "Capitalization")
            {
                deposit.Balance += interestSum;
            }
            else
            {
                deposit.Cards.Balance += interestSum;

                string cardHistoryDescription = String.Format("Interest payment in the amount of {0} ({1}).",
                    interestSum, deposit.DepositTerms.Currencies.Name);
                CardHistoryData.AddRecordToDbContext(deposit.Cards, cardHistoryDescription, dbContext);
            }
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

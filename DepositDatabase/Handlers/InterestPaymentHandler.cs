using System;
using System.Collections.Generic;
using System.Linq;
using DepositDatabase.Model;

namespace DepositDatabase.Handlers
{
    public class InterestPaymentHandler : DomainLogic.Handlers.IInterestPaymentHandler
    {
        private readonly DepositDbContext dbContext;

        public InterestPaymentHandler()
        {
            dbContext = new DepositDbContext();
        }

        public List<DomainLogic.Model.Deposit> GetActiveDeposits()
        {
            var deposits = (from d in dbContext.Deposits
                            where d.DepositState.Name != DomainLogic.Model.DepositState.ClosedDepositStateName
                            select d).ToList();

            return deposits.ToDomainLogic();
        }
        
        public string GetDepositStateName(int depositId)
        {
            var deposit = DepositsData.GetDepositById(depositId, dbContext);
            return deposit.DepositState.Name;
        }

        public void ExtendDeposit(int depositId, DateTime today)
        {
            var deposit = DepositsData.GetDepositById(depositId, dbContext);
            deposit.EndDate = today.AddMonths(deposit.DepositTerm.Months);
        }

        public void SetLastInterestPaymentDate(int depositId, DateTime date)
        {
            var deposit = DepositsData.GetDepositById(depositId, dbContext); 
            deposit.LastInterestPaymentDate = date;
        }

        public void SetDepositState(int depositId, string stateName)
        {
            var deposit = DepositsData.GetDepositById(depositId, dbContext);
            deposit.DepositState = DepositStatesData.GetStateByName(stateName, dbContext);
        }
        
        public void AddInterest(int depositId)
        {
            var deposit = DepositsData.GetDepositById(depositId, dbContext);

            const int daysInYear = 365;
            decimal interestRate = deposit.DepositTerm.InterestRate;
            DateTime today = DateTime.Today;

            byte daysCount = (byte)(today - (deposit.LastInterestPaymentDate ?? deposit.StartDate)).TotalDays;

            decimal interestSum = deposit.Balance * interestRate * daysCount / (100 * daysInYear);

            if (deposit.DepositWayOfAccumulation.Name == "Capitalization")
            {
                deposit.Balance += interestSum;
            }
            else
            {
                deposit.Card.Balance += interestSum;

                string cardHistoryDescription = String.Format("Interest payment in the amount of {0} ({1}).",
                    interestSum, deposit.DepositTerm.Currency.Name);
                CardHistoryData.AddRecordToDbContext(deposit.Card, cardHistoryDescription, dbContext);
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

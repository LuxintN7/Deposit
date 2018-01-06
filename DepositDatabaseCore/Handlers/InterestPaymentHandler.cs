using System;
using System.Collections.Generic;
using System.Linq;
using DepositDatabaseCore.Model;
using Microsoft.EntityFrameworkCore;

namespace DepositDatabaseCore.Handlers
{
    public class InterestPaymentHandler : DomainLogic.Handlers.IInterestPaymentHandler
    {
        private readonly DepositDbContext dbContext;

        public InterestPaymentHandler(DepositDbContext dbContext)
        {
            this.dbContext = dbContext;
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
            var deposit = dbContext.Deposits.Include(d => d.DepositState).First(d => d.Id == depositId);
            return deposit.DepositState.Name;
        }

        public void ExtendDeposit(int depositId, DateTime today)
        {
            var deposit = DepositsData.GetDepositById(dbContext, depositId);
            deposit.EndDate = today.AddMonths(deposit.DepositTerm.Months);
        }

        public void SetLastInterestPaymentDate(int depositId, DateTime date)
        {
            var deposit = DepositsData.GetDepositById(dbContext, depositId);
            deposit.LastInterestPaymentDate = date;
        }

        public void SetDepositState(int depositId, string stateName)
        {
            var deposit = dbContext.Deposits.Include(d => d.DepositState).First(d => d.Id == depositId);
            deposit.DepositState = DepositStatesData.GetStateByName(dbContext, stateName);
        }
        
        public void AddInterest(int depositId)
        {
            var deposit = dbContext.Deposits
                .Include(d => d.DepositTerm)
                .Include(d => d.DepositTerm.Currency)
                .Include(d => d.DepositWayOfAccumulation)
                .Include(d => d.Card)
                .First(d => d.Id == depositId);

            const int daysInYear = 365;
            decimal interestRate = deposit.DepositTerm.InterestRate;
            DateTime currentTime = DateTime.Now;

            var totalDays = (int)(currentTime - (deposit.LastInterestPaymentDate ?? deposit.StartDate)).TotalDays;

            decimal interestSum = deposit.Balance * interestRate * totalDays / (100 * daysInYear);

            if (deposit.DepositWayOfAccumulation.Name.Contains("capitalization"))
            {            
                deposit.Balance += interestSum;
            }
            else
            {
                deposit.Card.Balance += interestSum;

                string cardHistoryDescription = $"Interest payment in the amount of {Math.Round(interestSum, 2)} ({deposit.DepositTerm.Currency.Name}).";
                CardHistoryData.AddRecordToDbContext(dbContext, deposit.Card, cardHistoryDescription);
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

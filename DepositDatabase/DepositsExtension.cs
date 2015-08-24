using System.Collections.Generic;
using DepositDatabase.Model;

namespace DepositDatabase
{
    public static class DepositsExtension
    {
        public static DomainLogic.Model.Deposits ToDomainLogic(this Deposits deposit)
        {
            return new DomainLogic.Model.Deposits()
            {
                Id = deposit.Id,
                UserOwnerId = deposit.UserOwnerId,
                InitialAmount = deposit.InitialAmount,
                Balance = deposit.Balance,
                StartDate = deposit.StartDate,
                EndDate = deposit.EndDate,
                LastInterestPaymentDate = deposit.LastInterestPaymentDate,
                CardForAccumulationId = deposit.CardForAccumulationId,
                StateId = deposit.StateId,
                TermId = deposit.TermId,
                WayOfAccumulationId = deposit.WayOfAccumulationId,
                Name = deposit.Name
            };
        }

        public static List<DomainLogic.Model.Deposits> ToDomainLogic(this List<Deposits> list)
        {
            var deposits = new List<DomainLogic.Model.Deposits>();

            foreach (var deposit in list)
            {
                deposits.Add(deposit.ToDomainLogic());
            }

            return deposits;
        }
    }
}

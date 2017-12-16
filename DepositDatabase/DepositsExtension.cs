using System.Collections.Generic;
using DepositDatabase.Model;

namespace DepositDatabase
{
    public static class DepositsExtension
    {
        public static DomainLogic.Model.Deposit ToDomainLogic(this Deposit deposit)
        {
            return new DomainLogic.Model.Deposit()
            {
                Id = deposit.Id,
                UserOwnerId = deposit.UserOwnerId,
                InitialAmount = deposit.InitialAmount,
                Balance = deposit.Balance,
                StartDate = deposit.StartDate,
                EndDate = deposit.EndDate,
                LastInterestPaymentDate = deposit.LastInterestPaymentDate,
                CardForAccumulationId = deposit.CardId,
                StateId = deposit.DepositStateId,
                TermId = deposit.DepositTermId,
                WayOfAccumulationId = deposit.DepositWayOfAccumulationId,
                Name = deposit.Name
            };
        }

        public static List<DomainLogic.Model.Deposit> ToDomainLogic(this List<Deposit> list)
        {
            var deposits = new List<DomainLogic.Model.Deposit>();

            foreach (var deposit in list)
            {
                deposits.Add(deposit.ToDomainLogic());
            }

            return deposits;
        }
    }
}

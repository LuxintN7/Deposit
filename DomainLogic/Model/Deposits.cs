using System;
using System.ComponentModel.DataAnnotations;

namespace DomainLogic.Model
{
    public class Deposits
    {
        public int Id { get; set; }

        public string UserOwnerId { get; set; }
        public byte TermId { get; set; }

        [Display(Name = "Initial amount")]
        public decimal InitialAmount { get; set; }

        [Display(Name = "Balance")]
        public decimal Balance { get; set; }

        [Display(Name = "Start date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Last interest payment date")]
        public DateTime? LastInterestPaymentDate { get; set; }

        public byte StateId { get; set; }
        public byte WayOfAccumulationId { get; set; }
        public string CardForAccumulationId { get; set; }

        [Display(Name = "Deposit name")]
        public string Name { get; set; }

        [Display(Name = "Owner")]
        public virtual AspNetUsers AspNetUsers { get; set; }

        [Display(Name = "Card for accumulation")]
        public virtual Cards Cards { get; set; }

        [Display(Name = "State")]
        public virtual DepositStates DepositStates { get; set; }

        [Display(Name = "Deposit terms")]
        public virtual DepositTerms DepositTerms { get; set; }

        [Display(Name = "Way of accumulation")]
        public virtual DepositWaysOfAccumulation DepositWaysOfAccumulation { get; set; }
    }
}

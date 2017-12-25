using System;
using System.ComponentModel.DataAnnotations;

namespace DomainLogic.Model
{
    public class Deposit
    {
        public int Id { get; set; }

        public string UserOwnerId { get; set; }
        public int TermId { get; set; }

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
    }
}

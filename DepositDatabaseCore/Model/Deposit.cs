using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepositDatabaseCore.Model
{
    public class Deposit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string UserOwnerId { get; set; }
        public int DepositTermId { get; set; }

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

        public int DepositStateId { get; set; }
        public int DepositWayOfAccumulationId { get; set; }
        public string CardId { get; set; }

        [Display(Name = "Deposit name")]
        public string Name { get; set; }

        [Display(Name = "Owner")]
        public virtual AspNetUser UserOwner { get; set; }

        [Display(Name = "Card for accumulation")]
        public virtual Card Card { get; set; }

        [Display(Name = "State")]
        public virtual DepositState DepositState { get; set; }

        [Display(Name = "Deposit terms")]
        public virtual DepositTerm DepositTerm { get; set; }

        [Display(Name = "Way of accumulation")]
        public virtual DepositWayOfAccumulation DepositWayOfAccumulation { get; set; }
    }
}

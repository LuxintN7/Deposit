using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainLogic.Model
{
    public class DepositTerms
    {
        public DepositTerms()
        {
            this.Deposits = new HashSet<Deposits>();
        }

        public byte Id { get; set; }

        public byte CurrencyId { get; set; }

        [Display(Name = "Months")]
        public byte Months { get; set; }

        [Display(Name = "Interest rate")]
        public decimal InterestRate { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Currency")]
        public virtual Currencies Currencies { get; set; }

        public virtual ICollection<Deposits> Deposits { get; set; }
    }
}

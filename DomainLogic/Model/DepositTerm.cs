using System.ComponentModel.DataAnnotations;

namespace DomainLogic.Model
{
    public class DepositTerm
    {
        public int Id { get; set; }

        public int CurrencyId { get; set; }

        [Display(Name = "Months")]
        public byte Months { get; set; }

        [Display(Name = "Interest rate")]
        public decimal InterestRate { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}

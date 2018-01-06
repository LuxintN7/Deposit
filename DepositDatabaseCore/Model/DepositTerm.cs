using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepositDatabaseCore.Model
{
    public class DepositTerm
    {
        public DepositTerm()
        {
            Deposits = new HashSet<Deposit>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int CurrencyId { get; set; }

        [Display(Name = "Months")]
        public int Months { get; set; }

        [Display(Name = "Interest rate")]
        public decimal InterestRate { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Currency")]
        public virtual Currency Currency { get; set; }

        public virtual ICollection<Deposit> Deposits { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepositDatabase.Model
{
    public class DepositState
    {
        public DepositState()
        {
            Deposits = new HashSet<Deposit>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }

        [Display(Name = "State")]
        public string Name { get; set; }
    
        public virtual ICollection<Deposit> Deposits { get; set; }
    }
}

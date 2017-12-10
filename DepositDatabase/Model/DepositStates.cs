using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepositDatabase.Model
{
    public class DepositStates
    {
        public DepositStates()
        {
            Deposits = new HashSet<Deposits>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public byte Id { get; set; }

        [Display(Name = "State")]
        public string Name { get; set; }
    
        public virtual ICollection<Deposits> Deposits { get; set; }
    }
}

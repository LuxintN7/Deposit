using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepositDatabase.Model
{
    public class DepositWaysOfAccumulation
    {
        public DepositWaysOfAccumulation()
        {
            Deposits = new HashSet<Deposits>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public byte Id { get; set; }

        [Display(Name = "Way of accumulation")]
        public string Name { get; set; }
    
        public virtual ICollection<Deposits> Deposits { get; set; }
    }
}

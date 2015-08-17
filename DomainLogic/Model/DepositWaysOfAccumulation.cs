using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainLogic.Model
{
    public class DepositWaysOfAccumulation
    {
        public DepositWaysOfAccumulation()
        {
            this.Deposits = new HashSet<Deposits>();
        }

        public byte Id { get; set; }

        [Display(Name = "Way of accumulation")]
        public string Name { get; set; }
    
        public virtual ICollection<Deposits> Deposits { get; set; }
    }
}

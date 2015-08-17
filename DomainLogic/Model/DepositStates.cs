using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainLogic.Model
{
    public class DepositStates
    {
        public DepositStates()
        {
            this.Deposits = new HashSet<Deposits>();
        }

        public byte Id { get; set; }

        [Display(Name = "State")]
        public string Name { get; set; }
    
        public virtual ICollection<Deposits> Deposits { get; set; }
    }
}

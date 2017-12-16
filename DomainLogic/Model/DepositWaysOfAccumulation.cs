using System.ComponentModel.DataAnnotations;

namespace DomainLogic.Model
{
    public class DepositWayOfAccumulation
    {
        public byte Id { get; set; }

        [Display(Name = "Way of accumulation")]
        public string Name { get; set; }
    }
}

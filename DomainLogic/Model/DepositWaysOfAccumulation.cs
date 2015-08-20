using System.ComponentModel.DataAnnotations;

namespace DomainLogic.Model
{
    public class DepositWaysOfAccumulation
    {
        public byte Id { get; set; }

        [Display(Name = "Way of accumulation")]
        public string Name { get; set; }
    }
}

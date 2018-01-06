using System.ComponentModel.DataAnnotations;

namespace DomainLogic.Model
{
    public class DepositWayOfAccumulation
    {
        public int Id { get; set; }

        [Display(Name = "Way of accumulation")]
        public string Name { get; set; }
    }
}

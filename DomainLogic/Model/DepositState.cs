using System.ComponentModel.DataAnnotations;

namespace DomainLogic.Model
{
    public class DepositState
    {
        public const string OpenedDepositStateName = "Opened";
        public const string ClosedDepositStateName = "Closed";

        public int Id { get; set; }

        [Display(Name = "State")]
        public string Name { get; set; }
    }
}

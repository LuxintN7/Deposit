using System.ComponentModel.DataAnnotations;

namespace DomainLogic.Model
{
    public class DepositStates
    {
        public byte Id { get; set; }

        [Display(Name = "State")]
        public string Name { get; set; }
    }
}

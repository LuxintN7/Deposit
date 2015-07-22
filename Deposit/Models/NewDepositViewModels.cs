using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DepositDatabase;
using DepositDatabase.Model;

namespace Deposit.Models
{
    public class NewDepositViewModel : INewDeposit
    {
        private readonly List<Cards> cards;
        
        [Display(Name = "Card number")]
        public string CardId { get; set; }

        public int WayOfAccumulationId { get; set; }

        [Required]
        [RegularExpression("^[0-9]+([,][0-9]+)?$", ErrorMessage = "The field must contain only digits.")]
        [Display(Name = "Deposit amount")]
        public decimal Amount { get; set; }
        
        public IEnumerable<SelectListItem> Cards
        {
            get { return new SelectList(cards, "Id", "Id"); }
        }
        
        private readonly List<DepositWaysOfAccumulation> waysOfAccumulation;
        [Display(Name = "Way of accumulation")]
        public List<DepositWaysOfAccumulation> WaysOfAccumulation
        {
            get { return waysOfAccumulation; }
        }

        public NewDepositViewModel()
        {
            cards = new List<Cards>();
            waysOfAccumulation = new List<DepositWaysOfAccumulation>();
        }

        public NewDepositViewModel(List<Cards> cards, List<DepositWaysOfAccumulation> waysOfAccumulation)
        {
            this.cards = cards;
            this.waysOfAccumulation = waysOfAccumulation;
        }
    }

    public class MessageViewModel
    {
        public MessageViewModel(string message, bool isSuccessfull)
        {
            Message = message;
            IsSuccessfull = isSuccessfull;
        }

        public bool IsSuccessfull { get; set; }
        public string Message { get; set; }
    }
}


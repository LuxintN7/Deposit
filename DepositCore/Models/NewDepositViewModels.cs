using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DepositDatabaseCore;
using DomainLogic.Model;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DepositCore.Models
{
    public class NewDepositViewModel : INewDeposit
    {
        private readonly List<Card> cards;
        
        [Display(Name = "Card number")]
        public string CardId { get; set; }

        public int WayOfAccumulationId { get; set; }

        [Required]
        [RegularExpression("^[0-9]+([,][0-9]+)?$", ErrorMessage = "The field must contain only digits.")]
        [Display(Name = "Deposit amount")]
        public decimal Amount { get; set; }
        
        public IEnumerable<SelectListItem> Cards => new SelectList(cards, nameof(Card.Id), nameof(Card.Id));

        [Display(Name = "Way of accumulation")]
        public List<DepositWayOfAccumulation> WaysOfAccumulation { get; }

        public NewDepositViewModel()
        {
            cards = new List<Card>();
            WaysOfAccumulation = new List<DepositWayOfAccumulation>();
        }

        public NewDepositViewModel(List<Card> cards, List<DepositWayOfAccumulation> waysOfAccumulation)
        {
            this.cards = cards;
            this.WaysOfAccumulation = waysOfAccumulation;
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


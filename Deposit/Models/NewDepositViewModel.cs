using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DepositDatabase.Model;

namespace Deposit.Models
{
    public class NewDepositViewModel
    {
        private readonly List<Cards> _cards;
        
        [Display(Name = "Картка")]
        public string SelectedCardId { get; set; }

        public int SelectedWayOfAccumulationId { get; set; }

        [Required]
        [RegularExpression("[0-9]+", ErrorMessage = "Поле повинне містити лише цифри.")]
        [Display(Name = "Сума вкладу")]
        public string Sum { get; set; }
        
        public IEnumerable<SelectListItem> Cards
        {
            get { return new SelectList(_cards, "Id", "Id"); }
        }
        
        private readonly List<DepositWaysOfAccumulation> _waysOfAccumulation;
        [Display(Name = "Спосіб накопичення")]
        public List<DepositWaysOfAccumulation> WaysOfAccumulation
        {
            get { return _waysOfAccumulation; }
        }

        public NewDepositViewModel()
        {
            _cards = new List<Cards>();
            _waysOfAccumulation = new List<DepositWaysOfAccumulation>();
        }

        public NewDepositViewModel(List<Cards> cards, List<DepositWaysOfAccumulation> waysOfAccumulation)
        {
            _cards = cards;
            _waysOfAccumulation = waysOfAccumulation;
        }
    } 
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Deposit.Models
{
    public class NewDepositViewModel
    {
        public NewDepositViewModel()
        {
            _cards = new List<Cards>();
            _waysOfAccumulation = new List<WayOfAccumulation>();
        }

        public NewDepositViewModel(List<Cards> cards, List<WayOfAccumulation> waysOfAccumulation)
        {
            _cards = cards;
            _waysOfAccumulation = waysOfAccumulation;
        }
        
        [Display(Name = "Картка")]
        public string SelectedCardId { get; set; }

        public int SelectedWayOfAccumulationId { get; set; }

        [Required]
        [RegularExpression("[0-9]+", ErrorMessage = "Поле повинне містити лише цифри.")]
        [Display(Name = "Сума вкладу")]
        public string Sum { get; set; }
        
        private readonly List<Cards> _cards;
        public IEnumerable<SelectListItem> Cards
        {
            get { return new SelectList(_cards, "Id", "Id"); }
        }
        
        private readonly List<WayOfAccumulation> _waysOfAccumulation;
        [Display(Name = "Спосіб накопичення")]
        public List<WayOfAccumulation> WaysOfAccumulation
        {
            get { return _waysOfAccumulation; }
        }
    } 

    public class WayOfAccumulation
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}


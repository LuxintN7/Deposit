using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainLogic.Model
{
    public class Currencies
    {
        //public Currencies()
        //{
        //    this.Cards = new HashSet<Cards>();
        //    this.DepositTerms = new HashSet<DepositTerms>();
        //}

        public byte Id { get; set; }
        
        [Display(Name = "Currency")]
        public string Name { get; set; }

        [Display(Name = "Currency")]
        public string Abbreviation { get; set; }
    
        //public virtual ICollection<Cards> Cards { get; set; }
        //public virtual ICollection<DepositTerms> DepositTerms { get; set; }
    }
}

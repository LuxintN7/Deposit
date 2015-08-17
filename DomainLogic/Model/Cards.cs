using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainLogic.Model
{
    public class Cards
    {
        //public Cards()
        //{
        //    this.CardHistory = new HashSet<CardHistory>();
        //    this.Deposits = new HashSet<Deposits>();
        //}
    
        public string Id { get; set; }
        public string UserOwnerId { get; set; }

        [Display(Name = "Expiration month")]
        public string ExpirationMonth { get; set; }

        [Display(Name = "Expiration year")]
        public string ExpirationYear { get; set; }

        [Display(Name = "Secret code")]
        public string SecretCode { get; set; }

        public byte CurrencyId { get; set; }
        
        [Display(Name = "Balance")]
        public decimal Balance { get; set; }
        
        //[Display(Name = "Owner")]
        //public virtual AspNetUsers AspNetUsers { get; set; }

        //public virtual ICollection<CardHistory> CardHistory { get; set; }

        //[Display(Name = "Currency")]
        //public virtual Currencies Currencies { get; set; }

        //public virtual ICollection<Deposits> Deposits { get; set; }
    }
}

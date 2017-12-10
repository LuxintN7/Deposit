using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DepositDatabase.Model
{
    public class Cards
    {
        public Cards()
        {
            CardHistory = new HashSet<CardHistory>();
            Deposits = new HashSet<Deposits>();
        }
    
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
        
        [Display(Name = "Owner")]
        public virtual IdentityUser AspNetUsers { get; set; }

        public virtual ICollection<CardHistory> CardHistory { get; set; }

        [Display(Name = "Currency")]
        public virtual Currencies Currencies { get; set; }

        public virtual ICollection<Deposits> Deposits { get; set; }
    }
}

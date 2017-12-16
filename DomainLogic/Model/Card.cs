using System.ComponentModel.DataAnnotations;

namespace DomainLogic.Model
{
    public class Card
    {
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
    }
}

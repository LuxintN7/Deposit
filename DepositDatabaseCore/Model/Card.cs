using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepositDatabaseCore.Model
{
    public class Card
    {
        public Card()
        {
            CardHistoryRecords = new HashSet<CardHistory>();
            Deposits = new HashSet<Deposit>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        public string UserOwnerId { get; set; }

        [Display(Name = "Expiration month")]
        public string ExpirationMonth { get; set; }

        [Display(Name = "Expiration year")]
        public string ExpirationYear { get; set; }

        [Display(Name = "Secret code")]
        public string SecretCode { get; set; }

        public int CurrencyId { get; set; }
        
        [Display(Name = "Balance")]
        public decimal Balance { get; set; }
        
        [Display(Name = "Owner")]
        public virtual AspNetUser UserOwner { get; set; }

        [Display(Name = "Currency")]
        public virtual Currency Currency { get; set; }

        public virtual ICollection<CardHistory> CardHistoryRecords { get; set; }
        
        public virtual ICollection<Deposit> Deposits { get; set; }
    }
}

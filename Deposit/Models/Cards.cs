//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Deposit.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cards
    {
        public Cards()
        {
            this.CardOwnership = new HashSet<CardOwnership>();
            this.CardHistory = new HashSet<CardHistory>();
        }
    
        public string Id { get; set; }
        public string ExpirationMonth { get; set; }
        public string ExpirationYear { get; set; }
        public string SecretCode { get; set; }
        public decimal Balance { get; set; }
    
        public virtual ICollection<CardOwnership> CardOwnership { get; set; }
        public virtual Currencies Currencies { get; set; }
        public virtual ICollection<CardHistory> CardHistory { get; set; }
    }
}

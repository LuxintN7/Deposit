using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DepositDatabaseCore.Model
{
    public class Currency
    {
        public Currency()
        {
            Cards = new HashSet<Card>();
            DepositTerms = new HashSet<DepositTerm>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Display(Name = "Currency")]
        public string Name { get; set; }

        [Display(Name = "Currency")]
        public string Abbreviation { get; set; }
    
        public virtual ICollection<Card> Cards { get; set; }
        public virtual ICollection<DepositTerm> DepositTerms { get; set; }

        public static Currency GetById(int id, DepositDbContext context)
        {
            return context.Currencies.FirstOrDefault(c => c.Id == id);
        }
    }
}

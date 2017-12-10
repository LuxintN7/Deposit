using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DepositDatabase.Model
{
    public class Currencies
    {
        public Currencies()
        {
            Cards = new HashSet<Cards>();
            DepositTerms = new HashSet<DepositTerms>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public byte Id { get; set; }
        
        [Display(Name = "Currency")]
        public string Name { get; set; }

        [Display(Name = "Currency")]
        public string Abbreviation { get; set; }
    
        public virtual ICollection<Cards> Cards { get; set; }
        public virtual ICollection<DepositTerms> DepositTerms { get; set; }

        public static Currencies GetById(byte id, DepositDbContext context)
        {
            return context.Currencies.FirstOrDefault(c => c.Id == id);
        }
    }
}

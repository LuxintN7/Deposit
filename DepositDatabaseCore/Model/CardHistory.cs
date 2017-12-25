using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepositDatabaseCore.Model
{
    public class CardHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CardId { get; set; }
        public DateTime DateTime { get; set; }
        public string Desription { get; set; }
    
        public virtual Card Card { get; set; }
    }
}

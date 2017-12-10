using System;

namespace DepositDatabase.Model
{
    public partial class CardHistory
    {
        public int Id { get; set; }
        public string IdCard { get; set; }
        public DateTime DateTime { get; set; }
        public string Desription { get; set; }
    
        public virtual Cards Cards { get; set; }
    }
}

using System;

namespace DomainLogic.Model
{
    public class CardHistory
    {
        public int Id { get; set; }
        public string IdCard { get; set; }
        public DateTime DateTime { get; set; }
        public string Desription { get; set; }
    }
}

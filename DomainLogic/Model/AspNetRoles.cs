using System.Collections.Generic;

namespace DomainLogic.Model
{
    public class AspNetRoles
    {
        public AspNetRoles()
        {
            this.AspNetUsers = new HashSet<AspNetUsers>();
        }
    
        public string Id { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<AspNetUsers> AspNetUsers { get; set; }
    }
}

using System.Collections.Generic;

namespace Coin.Web.Entities
{
    public class Acl : Entity
    {
        public string Name { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
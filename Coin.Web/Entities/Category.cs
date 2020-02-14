using System.Collections.Generic;

namespace Coin.Web.Entities
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long ParentId { get; set; }
        public bool Published { get; set; }
        public ICollection<ProductCategory> Products { get; set; }
    }
}
using System;
using System.Collections.Generic;

namespace Coin.Web.Entities
{
    public class Order : Entity
    {
        public bool IsOrdered { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime OrderPlaced { get; set; }
        public long CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
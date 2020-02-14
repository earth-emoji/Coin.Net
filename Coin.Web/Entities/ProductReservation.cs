using System;

namespace Coin.Web.Entities
{
    public class ProductReservation : Entity
    {
        public DateTime Date { get; set; }
        public string Duration { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }

        public long OrderId { get; set; }
    }
}
namespace Coin.Web.Entities
{
    public class OrderItem : Entity
    {
        public bool IsOrdered { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }

        public long OrderId { get; set; }
        public Order Order { get; set; }

        public long ProductId { get; set; }
        public Product Product { get; set; }
    }
}
namespace Coin.Web.Entities
{
    public class ProductAttributeValue
    {
        public long Id { get; set; }

        public int StockQuantity { get; set; }

        public decimal? OverriddenPrice { get; set; }
        
        public long AttributeValueId { get; set; }
        public AttributeValue AttributeValue { get; set; }

        public long ProductId { get; set; }
        public Product Product { get; set; }
    }
}
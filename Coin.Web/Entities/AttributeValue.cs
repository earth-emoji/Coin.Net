namespace Coin.Web.Entities
{
    public class AttributeValue : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public long AttributeId { get; set; }
        public Attribute Attribute { get; set; }
    }
}
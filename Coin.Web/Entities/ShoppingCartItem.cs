using System.ComponentModel.DataAnnotations;

namespace Coin.Web.Entities
{
    public class ShoppingCartItem : Entity
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
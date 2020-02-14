using System;
using System.Collections.Generic;
using System.Linq;
using Coin.Web.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Coin.Web.Entities
{
    public class ShoppingCart
	{
		private readonly ApplicationDbContext _context;

		public ShoppingCart(ApplicationDbContext context)
		{
			_context = context;
		}

		public string Id { get; set; }
		public IEnumerable<ShoppingCartItem> ShoppingCartItems { get; set; }

		public static ShoppingCart GetCart(IServiceProvider services)
		{
			ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
			var context = services.GetService<ApplicationDbContext>();
			string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

			session.SetString("CartId", cartId);
			return new ShoppingCart(context) { Id = cartId };
		}

		public bool AddToCart(Product product, int quantity)
		{
			if(product.StockQuantity == 0 || quantity == 0)
			{
				return false;
			}
			
			var shoppingCartItem = _context.ShoppingCartItems.SingleOrDefault(
				s => s.Product.Id == product.Id && s.ShoppingCartId == Id);
            
            var isValidAmount = true;

			if (shoppingCartItem == null)
			{
                if (quantity > product.StockQuantity)
                {
                    isValidAmount = false;
                }

                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = Id,
                    Product = product,
                    Quantity = Math.Min(product.StockQuantity, quantity)
				};

				_context.ShoppingCartItems.Add(shoppingCartItem);
			}
			else
			{
                if(product.StockQuantity - shoppingCartItem.Quantity - quantity >= 0)
                {
                    shoppingCartItem.Quantity +=  quantity;
                }
                else
                {
					shoppingCartItem.Quantity += (product.StockQuantity - shoppingCartItem.Quantity);
                    isValidAmount = false;
                }
            }


			_context.SaveChanges();
            return isValidAmount;
		}

		public int RemoveFromCart(Product product)
		{
			var shoppingCartItem = _context.ShoppingCartItems.SingleOrDefault(
				s => s.Product.Id == product.Id && s.ShoppingCartId == Id);

			int localQuantity = 0;

			if (shoppingCartItem != null)
			{
				if (shoppingCartItem.Quantity > 1)
				{
					shoppingCartItem.Quantity--;
					localQuantity = shoppingCartItem.Quantity;
				}
				else
				{
					_context.ShoppingCartItems.Remove(shoppingCartItem);
				}
			}

			_context.SaveChanges();
			return localQuantity;
		}

		public IEnumerable<ShoppingCartItem> GetShoppingCartItems()
		{
			return ShoppingCartItems ??
				   (ShoppingCartItems = _context.ShoppingCartItems.Where(c => c.ShoppingCartId == Id)
					   .Include(s => s.Product));
		}

		public void ClearCart()
		{
			var cartItems = _context
				.ShoppingCartItems
				.Where(cart => cart.ShoppingCartId == Id);

			_context.ShoppingCartItems.RemoveRange(cartItems);
			_context.SaveChanges();
		}

		public decimal GetShoppingCartTotal()
		{
			return _context.ShoppingCartItems.Where(c => c.ShoppingCartId == Id)
				.Select(c => c.Product.Price * c.Quantity).Sum();
		}

	}
}
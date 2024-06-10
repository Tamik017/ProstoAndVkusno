using Microsoft.EntityFrameworkCore;
using ProstoAndVkusno.DBContext;

namespace ProstoAndVkusno.Data.Models
{
	public class ShopCart
	{
		private readonly ApplicationContext applicationContext;
		public ShopCart(ApplicationContext applicationContext)
		{
			this.applicationContext = applicationContext;
		}

		public string ShopCartId { get; set; }
		public List<ShopCartItem> ListShopItems { get; set;}

		public static ShopCart GetCart(IServiceProvider services)
		{
			ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
			var context = services.GetService<ApplicationContext>();
			string shopCartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

			session.SetString("CartId", shopCartId);

			return new ShopCart(context) { ShopCartId = shopCartId };
		}

		public void AddToCart(Product product)
		{
			applicationContext._shopCartItems.Add(new ShopCartItem
			{
				ShopCartId = ShopCartId,
				product = product,
				price = product.price
			});

			applicationContext.SaveChanges();
		}

		public void ClearCart()
		{
			var cartItems = applicationContext._shopCartItems.Where(c => c.ShopCartId == ShopCartId).ToList();

			foreach (var item in cartItems)
			{
				applicationContext._shopCartItems.Remove(item);
			}

			applicationContext.SaveChanges();
		}

        public void RemoveFromCart(int itemId)
        {
            var cartItem = applicationContext._shopCartItems.FirstOrDefault(c => c.ID == itemId);
            if (cartItem != null)
            {
                applicationContext._shopCartItems.Remove(cartItem);
                applicationContext.SaveChanges();
            }
        }

        public List<ShopCartItem> GetShopItems()
		{
			return applicationContext._shopCartItems.Where(c => c.ShopCartId == ShopCartId).Include(s => s.product).ToList();
		}
	}
}

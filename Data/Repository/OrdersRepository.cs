using ProstoAndVkusno.Data.Interfaces;
using ProstoAndVkusno.Data.Models;
using ProstoAndVkusno.DBContext;

namespace ProstoAndVkusno.Data.Repository
{
	public class OrdersRepository : IAllOrders
	{
		private readonly ApplicationContext applicationContext;
		private readonly ShopCart shopCart;

		public OrdersRepository(ApplicationContext applicationContext, ShopCart shopCart)
		{
			this.applicationContext = applicationContext;
			this.shopCart = shopCart;
		}

		public void createOrder(Order order)
		{
			order.orderTime = DateTime.Now;
			applicationContext._order.Add(order);
			applicationContext.SaveChanges();

			var items = shopCart.ListShopItems;
			foreach (var element in items)
			{
				var orderDetail = new OrderDetail()
				{
					productID = element.product.ID,
					orderID = order.ID, 
					Price = element.product.price,
				};
				order.orderDetails.Add(orderDetail);
				applicationContext._orderDetail.Add(orderDetail);
			}
			applicationContext.SaveChanges();
		}
	}
}
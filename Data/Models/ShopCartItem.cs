namespace ProstoAndVkusno.Data.Models
{
	public class ShopCartItem
	{
		public int ID { get; set; }
		public Product product { get; set; }
		public decimal price { get; set; }

		public string ShopCartId { get; set; }
	}
}

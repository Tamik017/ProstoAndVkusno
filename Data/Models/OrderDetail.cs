namespace ProstoAndVkusno.Data.Models
{
	public class OrderDetail
	{
		public int ID { get; set; }
		public int orderID { get; set; }
		public int productID { get; set; }
		public decimal Price { get; set; }
		public virtual Product product { get; set; }
		public virtual Order order { get; set; }

	}
}

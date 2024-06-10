using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace ProstoAndVkusno.Data.Models
{
	public class Order
	{
		[BindNever]
		public int ID { get; set; }

		[StringLength(100)]
		[Required(ErrorMessage = "Имя обязательно!")]
		public string Name { get; set; }

		[StringLength(100)]
		[Required(ErrorMessage = "Фамилия обязательно!")]
		public string surName { get; set; }

		[StringLength(100)]
		[Required(ErrorMessage = "Адрес обязателен!")]
		public string Adress { get; set; }

		[StringLength(10)]
		[Required(ErrorMessage = "Необходимо заполнить!")]
		[Phone(ErrorMessage = "Некорректный номер телефона")]
		public string Phone { get; set; }

		[StringLength(100)]
		[Required(ErrorMessage = "Необходимо заполнить!")]
		[EmailAddress(ErrorMessage = "Некорректный адрес электронной почты")]
		public string Email { get; set; }

		public string Status { get; set; } = "в обработке";

		[BindNever]
		[ScaffoldColumn(false)] // чтобы не отображалось даже если пользователь зайдет в исходный код
		public DateTime orderTime { get; set; }

		public List<OrderDetail> orderDetails { get; set; } = new List<OrderDetail>();
	}
}

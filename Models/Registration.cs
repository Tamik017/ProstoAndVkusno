using System.ComponentModel.DataAnnotations;

namespace ProstoAndVkusno.Models
{
	public class Registration
	{
		[Required(ErrorMessage = "Логин обязателен!")]
		[StringLength(50, ErrorMessage = "Логин должен быть не длиннее 50 символов!")]
		public string Login { get; set; }

		[Required(ErrorMessage = "Email обязателен!")]
		[EmailAddress(ErrorMessage = "Неверный формат Email!")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Пароль обязателен!")]
		[DataType(DataType.Password)]
		[StringLength(100, MinimumLength = 6, ErrorMessage = "Пароль должен быть длиннее 8 символов!")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Неправильный пароль :/")]
		public string ConfirmPassword { get; set; }
		public string Role { get; set; } = "user";
	}
}

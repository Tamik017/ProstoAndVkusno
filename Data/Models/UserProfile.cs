using System.ComponentModel.DataAnnotations;

namespace ProstoAndVkusno.Data.Models
{
    public class UserProfile
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }

        [Required(ErrorMessage = "Введи старый пароль!")]
        [StringLength(100, MinimumLength = 8)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Введи новый пароль!")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Пароль должен быть длиннее 8 символов!")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Пароли не совпадают!")]
        public string ConfirmPassword { get; set; }
    }
}

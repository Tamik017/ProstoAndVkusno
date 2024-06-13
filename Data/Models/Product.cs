using System.ComponentModel.DataAnnotations;

namespace ProstoAndVkusno.Data.Models
{
    public class Product
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Название продукта обязательно")]
        [MaxLength(255, ErrorMessage = "Название продукта не должно превышать 255 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Краткое описание продукта обязательно")]
        public string ShortDesc { get; set; }

        [Required(ErrorMessage = "Полное описание продукта обязательно")]
        public string LongDesc { get; set; }

        [Required(ErrorMessage = "Ссылка на изображение обязательна")]
        public string img { get; set; }

        [Required(ErrorMessage = "Цена продукта обязательна")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Цена должна быть больше 0")]
        public decimal price { get; set; }

        [Required(ErrorMessage = "Поле 'isFavourite' обязательно")]
        public bool isFavourite { get; set; }

        [Required(ErrorMessage = "Поле 'available' обязательно")]
        public bool available { get; set; }

        [Required(ErrorMessage = "Категория продукта обязательна")]
        public int categoryID { get; set; }

        public virtual Category Category { get; set; }

        public override string ToString()
        {
            return $"Id:{ID} Name:{Name} ShortDesc: {ShortDesc} LongDesc:{LongDesc} img:{img} price:{price} isFavourite:{isFavourite} available:{available} categoryID:{categoryID}";
        }
    }
}
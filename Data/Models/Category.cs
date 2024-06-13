using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace ProstoAndVkusno.Data.Models
{
    public class Category
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Название категории обязательно")]
        [MaxLength(255, ErrorMessage = "Название категории не должно превышать 255 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Описание категории обязательно")]
        public string Description { get; set; }

        [JsonIgnore] // чтобы не было бесконечного цикла
        public List<Product> Products { get; set; }
    }
}
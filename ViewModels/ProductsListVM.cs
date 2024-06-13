using ProstoAndVkusno.Data.Models;

namespace ProstoAndVkusno.ViewModels
{
    public class ProductsListVM
    {
        public IEnumerable<Product> allProducts {  get; set; } //Свойство для хранения всех товаров
        public string currentCategory { get; set; } //Свойство для хранения текущей категории
    }
}

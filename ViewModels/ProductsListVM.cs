using ProstoAndVkusno.Data.Models;

namespace ProstoAndVkusno.ViewModels
{
    public class ProductsListVM
    {
        public IEnumerable<Product> allProducts {  get; set; }
        public string currentCategory { get; set; }
    }
}

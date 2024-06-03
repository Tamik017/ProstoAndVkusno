using ProstoAndVkusno.Models;

namespace ProstoAndVkusno.Interfaces
{
    public interface IAllProducts
    {
        IEnumerable<Product> GetProducts { get; }
        IEnumerable<Product> GetFavProducts { get; set;}
        Product GetProductById(int productId);
    }
}

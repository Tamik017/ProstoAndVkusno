using ProstoAndVkusno.Data.Models;

namespace ProstoAndVkusno.Data.Interfaces
{
    public interface IAllProducts
    {
        IEnumerable<Product> GetProducts { get; }
        IEnumerable<Product> GetFavProducts { get; }
        Product GetProductById(int productId);
    }
}

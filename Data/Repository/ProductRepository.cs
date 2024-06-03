using Microsoft.EntityFrameworkCore;
using ProstoAndVkusno.Data.Interfaces;
using ProstoAndVkusno.Data.Models;
using ProstoAndVkusno.DBContext;

namespace ProstoAndVkusno.Data.Repository
{
    public class ProductRepository : IAllProducts
    {
        private readonly ApplicationContext applicationContext;

        public ProductRepository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public IEnumerable<Product> GetProducts => applicationContext._products.Include(c => c.Category);

        public IEnumerable<Product> GetFavProducts => applicationContext._products.Where(p => p.isFavourite).Include(c => c.Category);

        public Product GetProductById(int productId) => applicationContext._products.FirstOrDefault(p => p.ID == productId);
    }
}

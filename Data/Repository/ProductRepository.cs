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

        //Свойство для получения всех продуктов и информации о категории
        public IEnumerable<Product> GetProducts => applicationContext._products.Include(c => c.Category);

        //Свойство для получения избранных продуктов и информации о категории
        public IEnumerable<Product> GetFavProducts => applicationContext._products.Where(p => p.isFavourite).Include(c => c.Category);

        //Метод для получения продукта по его ID
        public Product GetProductById(int productId) => applicationContext._products.FirstOrDefault(p => p.ID == productId);
    }
}

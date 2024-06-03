using ProstoAndVkusno.Data.Interfaces;
using ProstoAndVkusno.Data.Models;
using ProstoAndVkusno.DBContext;

namespace ProstoAndVkusno.Data.Repository
{
    public class CategoryRepository : IProductCategory
    {
        private readonly ApplicationContext applicationContext;

        public CategoryRepository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public IEnumerable<Category> GetCategories => applicationContext._categories;
    }
}

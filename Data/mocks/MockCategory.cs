using ProstoAndVkusno.Data.Interfaces;
using ProstoAndVkusno.Data.Models;

namespace ProstoAndVkusno.Data.mocks
{
    public class MockCategory : IProductCategory
    {
        public IEnumerable<Category> GetCategories
        {
            get
            {
                return new List<Category>
                {
                new Category {Name = "Пицца", Description = "Вкусная пицца"},
                new Category {Name = "Паста", Description = "Вкусная паста"},
                new Category {Name = "Фастфуд", Description = "Вкусный фастфуд"},
                new Category {Name = "Сок", Description = "Вкусный сок"},
                new Category {Name = "Соусы", Description = "Вкусные соусы"}
                };

            }

        }
    }
}

using ProstoAndVkusno.Data.Interfaces;
using ProstoAndVkusno.Data.Models;

namespace ProstoAndVkusno.Data.Mocks
{
    public class MockCategory : IProductCategory
    {
        public IEnumerable<Category> GetCategories
        {
            get
            {
                return new List<Category>
                {
                    new Category { ID = 1, Name = "Пицца" },
                    new Category { ID = 2, Name = "Паста" },
                    new Category { ID = 3, Name = "Фастфуд" },
                    new Category { ID = 4, Name = "Напитки" },
                    new Category { ID = 5, Name = "Соусы" }
                };
            }
        }

        // Добавлен метод GetCategory
        public Category GetCategory(string categoryName)
        {
            // Возвращаем категорию по имени
            // (Дополните логику поиска, если нужно)
            return GetCategories.FirstOrDefault(c => c.Name == categoryName);
        }
    }
}
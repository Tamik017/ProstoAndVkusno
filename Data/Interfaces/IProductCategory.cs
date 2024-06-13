using ProstoAndVkusno.Data.Models;

namespace ProstoAndVkusno.Data.Interfaces
{
    public interface IProductCategory
    {
        IEnumerable<Category> GetCategories { get; } // Получение всех категорий

    }
}
using ProstoAndVkusno.Models;

namespace ProstoAndVkusno.Interfaces
{
    interface IProductCategory
    {
        IEnumerable<Category> GetCategories {  get; }
    }
}

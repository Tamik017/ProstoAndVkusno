using Microsoft.AspNetCore.Mvc;
using ProstoAndVkusno.Data.Interfaces;
using ProstoAndVkusno.Data.Models;
using ProstoAndVkusno.ViewModels;

namespace ProstoAndVkusno.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IAllProducts _allProducts;
        private readonly IProductCategory _productCategory;

        public ProductsController(IAllProducts allProducts, IProductCategory productCategory)
        {
            _allProducts = allProducts;
            _productCategory = productCategory;
        }

        [Route("Products/List")]
		[Route("Products/List/{category}")]
		public ViewResult List(string category)
        {
            string _category = category;
            IEnumerable<Product> products = null;
            string currentCategory = "";
            if(string.IsNullOrEmpty(category)) //категории
            {
                products = _allProducts.GetProducts.OrderBy(i => i.ID);
            }
            else
            {
                if(string.Equals("pizza", category, StringComparison.OrdinalIgnoreCase))
                {
                    products = _allProducts.GetProducts.Where(i => i.Category.Name.Equals("Пицца")).OrderBy(i => i.ID);
                    currentCategory = "Пицца";
                }
                else if(string.Equals("pasta", category, StringComparison.OrdinalIgnoreCase))
				{
					products = _allProducts.GetProducts.Where(i => i.Category.Name.Equals("Паста")).OrderBy(i => i.ID);
					currentCategory = "Паста";
				}
				else if (string.Equals("fastfood", category, StringComparison.OrdinalIgnoreCase))
				{
					products = _allProducts.GetProducts.Where(i => i.Category.Name.Equals("Фастфуд")).OrderBy(i => i.ID);
					currentCategory = "Фастфуд";
				}
				else if (string.Equals("drink", category, StringComparison.OrdinalIgnoreCase))
				{
					products = _allProducts.GetProducts.Where(i => i.Category.Name.Equals("Напитки")).OrderBy(i => i.ID);
					currentCategory = "Напитки";
				}
				else if (string.Equals("sauce", category, StringComparison.OrdinalIgnoreCase))
				{
					products = _allProducts.GetProducts.Where(i => i.Category.Name.Equals("Соусы")).OrderBy(i => i.ID);
					currentCategory = "Соусы";
				}
			}

			var productObj = new ProductsListVM
			{
				allProducts = products,
				currentCategory = currentCategory
			};

			ViewBag.Title = "Старинца с продуктами";

            return View(productObj);
        }
    }
}

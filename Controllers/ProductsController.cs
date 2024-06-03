using Microsoft.AspNetCore.Mvc;
using ProstoAndVkusno.Data.Interfaces;
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

        public ViewResult List()
        {
            ViewBag.Title = "Старинца с продуктами";
            ProductsListVM PLVM = new ProductsListVM();
            PLVM.allProducts = _allProducts.GetProducts;
            PLVM.currentCategory = "Продукты";

            return View(PLVM);
        }
    }
}

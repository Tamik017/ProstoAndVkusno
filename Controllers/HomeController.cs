using Microsoft.AspNetCore.Mvc;
using ProstoAndVkusno.Data.Interfaces;
using ProstoAndVkusno.ViewModels;

namespace ProstoAndVkusno.Controllers
{
    public class HomeController : Controller
    {
		private readonly IAllProducts _productRepository;
		public HomeController(IAllProducts productRepository)
		{
			_productRepository = productRepository;
		}

        public ViewResult Index()
        {
            var homeProducts = new HomeVM
            {
                favProduct = _productRepository.GetFavProducts
			};
            return View(homeProducts);
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult Index(string studentName, string groupName)
        //{
        //    return View(new List<string>() { studentName, groupName });
        //}
    }
}

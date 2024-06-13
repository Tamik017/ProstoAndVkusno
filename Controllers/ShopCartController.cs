using Microsoft.AspNetCore.Mvc;
using ProstoAndVkusno.Data.Interfaces;
using ProstoAndVkusno.Data.Models;
using ProstoAndVkusno.Data.Repository;
using ProstoAndVkusno.ViewModels;

namespace ProstoAndVkusno.Controllers
{
    public class ShopCartController : Controller
    {
        private readonly IAllProducts _productRepository;
        private readonly ShopCart _shopCart;

        public ShopCartController(IAllProducts productRepository, ShopCart shopCart)
        {
            _productRepository = productRepository;
            _shopCart = shopCart;
        }

        public ViewResult Index()
        {
            var items = _shopCart.GetShopItems();
            _shopCart.ListShopItems = items;

            var obj = new ShopCartVM {
                shopCart = _shopCart
            };

            return View(obj);
        }

        [HttpPost]
        public IActionResult addToCart(int id)
        {
            var item = _productRepository.GetProducts.FirstOrDefault(i => i.ID == id);
            if (item != null)
            {
                _shopCart.AddToCart(item);
                _shopCart.ListShopItems = _shopCart.GetShopItems();
                var cartVM = new ShopCartVM { shopCart = _shopCart };

                return PartialView("_ShopCartPartial", cartVM);
            }

            return Json(new { success = false, message = "Товар не найден" });
        }

        public RedirectToActionResult RemoveFromCart(int itemId)
		{
			_shopCart.RemoveFromCart(itemId);
			return RedirectToAction("Index"); 
		}
	}
}

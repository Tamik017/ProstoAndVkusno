using Microsoft.AspNetCore.Mvc;
using ProstoAndVkusno.Data.Interfaces;
using ProstoAndVkusno.Data.Models;
using ProstoAndVkusno.DBContext;
using System.Diagnostics.Eventing.Reader;

namespace ProstoAndVkusno.Controllers
{
	public class OrderController : Controller
	{
		private readonly IAllOrders allOrders;
		private readonly ShopCart shopCart;

		public OrderController(IAllOrders allOrders, ShopCart shopCart)
		{
			this.allOrders = allOrders;
			this.shopCart = shopCart;

		}

		public IActionResult Checkout() 
		{
			return View();
		}

		[HttpPost]
		public IActionResult Checkout(Order order)
		{
			shopCart.ListShopItems = shopCart.GetShopItems();

			if (shopCart.ListShopItems.Count == 0)
			{
				TempData["ErrorMessage"] = "В корзине должны быть товары!";
				return RedirectToAction("List", "Products");
			}

			if (ModelState.IsValid) // Проверяем валидацию модели
			{
				allOrders.createOrder(order);
				shopCart.ClearCart(); // Очищаем корзину после принятия заказа
				return RedirectToAction("Complete"); 
			}
			else
			{
				return View(order); 
			}
		}

		public IActionResult Complete() 
		{
			ViewBag.Message = "Заказ успешно обработан";

			return View();
		}
	}
}

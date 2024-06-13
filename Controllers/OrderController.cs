using Microsoft.AspNetCore.Mvc;
using ProstoAndVkusno.Data.Interfaces;
using ProstoAndVkusno.Data.Models;
using ProstoAndVkusno.DBContext;
using System.Diagnostics.Eventing.Reader;

namespace ProstoAndVkusno.Controllers
{
    public class OrderController : Controller
    {
        // Внедрение зависимостей для работы с заказами и корзиной
        private readonly IAllOrders allOrders;
        private readonly ShopCart shopCart;

        // Конструктор для инициализации зависимостей
        public OrderController(IAllOrders allOrders, ShopCart shopCart)
        {
            this.allOrders = allOrders;
            this.shopCart = shopCart;
        }

        // Страница оформления заказа
        public IActionResult Checkout()
        {
            return View();
        }

        // Обработка формы оформления заказа
        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            shopCart.ListShopItems = shopCart.GetShopItems();

            // Проверка, есть ли товары в корзине
            if (shopCart.ListShopItems.Count == 0)
            {
                TempData["ErrorMessage"] = "В корзине должны быть товары!";
                return RedirectToAction("List", "Products");
            }

            // Проверка валидации модели
            if (ModelState.IsValid)
            {
                allOrders.createOrder(order);
                shopCart.ClearCart();
                return RedirectToAction("Complete");
            }
            else
            {
                return View(order);
            }
        }

        // Страница подтверждения заказа
        public IActionResult Complete()
        {
            ViewBag.Message = "Заказ успешно обработан";
            return View();
        }
    }
}
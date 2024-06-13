using Microsoft.AspNetCore.Mvc;
using ProstoAndVkusno.Data.Interfaces;
using ProstoAndVkusno.Data.Models;
using ProstoAndVkusno.ViewModels;

namespace ProstoAndVkusno.Controllers
{
    public class ProductsController : Controller
    {
        // Внедрение зависимостей для работы с товарами и категориями
        private readonly IAllProducts _allProducts;
        private readonly IProductCategory _productCategory;

        // Конструктор для инициализации зависимостей
        public ProductsController(IAllProducts allProducts, IProductCategory productCategory)
        {
            _allProducts = allProducts;
            _productCategory = productCategory;
        }

        // Метод для отображения списка товаров
        // Использует атрибут маршрутизации для создания нескольких маршрутов
        [Route("Products/List")] // Базовый маршрут
        [Route("Products/List/{category}")] // Маршрут с параметром "category"
        public ViewResult List(string category)
        {
            // Сохраняем категорию для использования в представлении
            string _category = category;
            IEnumerable<Product> products = null;
            string currentCategory = "";

            // Обработка запроса без категории
            if (string.IsNullOrEmpty(category))
            {
                // Получаем все товары, отсортированные по ID
                products = _allProducts.GetProducts.OrderBy(i => i.ID);
            }
            else
            {
                // Обработка запроса с категорией
                if (string.Equals("pizza", category, StringComparison.OrdinalIgnoreCase))
                {
                    // Фильтрация товаров по категории "Пицца"
                    products = _allProducts.GetProducts.Where(i => i.Category.Name.Equals("Пицца")).OrderBy(i => i.ID);
                    currentCategory = "Пицца";
                }
                // Аналогичная обработка для остальных категорий
                else if (string.Equals("pasta", category, StringComparison.OrdinalIgnoreCase))
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

            // Фильтрация по доступности
            products = products.Where(p => p.available).OrderBy(i => i.ID);

            // Создание модели представления для передачи данных в представление
            var productObj = new ProductsListVM
            {
                allProducts = products,
                currentCategory = currentCategory
            };

            // Установка заголовка страницы
            ViewBag.Title = "Старинца с продуктами";

            // Возвращение представления с моделью
            return View(productObj);
        }
    }
}
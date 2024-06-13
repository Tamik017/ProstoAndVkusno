using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ProstoAndVkusno.Data.Models;
using ProstoAndVkusno.DBContext;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProstoAndVkusno.Controllers
{
    // Доступ только для администраторов
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationContext _context;

        // Инициализация контекста базы данных
        public AdminController(ApplicationContext context)
        {
            _context = context;
        }

        // Пустая страница административной панели
        [HttpGet]
        public IActionResult Admin()
        {
            return View();
        }

        // Отображение списка пользователей
        [HttpGet]
        public IActionResult Users()
        {
            var users = _context._users.ToList();
            return View(users);
        }

        // Отображение списка товаров
        [HttpGet]
        public IActionResult Products()
        {
            var products = _context._products.Include(p => p.Category).ToList();
            return View(products);
        }

        // Получение информации о пользователе (JSON)
        [HttpGet]
        public IActionResult GetUser(int userId)
        {
            var user = _context._users.Find(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Json(user);
        }

        // Страница редактирования пользователя
        [HttpGet]
        public IActionResult EditUser(int id)
        {
            var user = _context._users.FirstOrDefault(u => u.ID == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // Обработка формы редактирования пользователя
        [HttpPost]
        public async Task<IActionResult> EditUser(int id, Users model)
        {
            // Пароль не редактируется
            ModelState.Remove("Password");
            if (ModelState.IsValid)
            {
                var user = await _context._users.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                // Обновление информации о пользователе
                user.Login = model.Login;
                user.Email = model.Email;
                user.Role = model.Role;
                _context._users.Update(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Users");
            }
            return View(model);
        }

        // Получение информации о товаре (JSON)
        [HttpGet]
        public IActionResult GetProduct(int productId)
        {
            var product = _context._products.Include(p => p.Category).FirstOrDefault(p => p.ID == productId);
            if (product == null)
            {
                return NotFound();
            }
            return Json(product, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }

        // Страница создания товара
        [HttpGet]
        public IActionResult CreateProduct()
        {
            // Список категорий для выбора
            ViewBag.Categories = _context._categories.Select(c => new SelectListItem
            {
                Value = c.ID.ToString(),
                Text = c.Name
            }).ToList();
            return View(new Product());
        }

        // Обработка формы создания товара
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            // Временная проверка (в реальном коде использовать ModelState.IsValid)
            bool temp = true;
            if (temp)
            {
                _context._products.Add(product);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Товар успешно добавлен.";
                return RedirectToAction("Products");
            }
            // Список категорий для выбора
            ViewBag.Categories = _context._categories.Select(c => new SelectListItem
            {
                Value = c.ID.ToString(),
                Text = c.Name
            }).ToList();
            return View(product);
        }

        // Страница редактирования товара
        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            var product = _context._products.Include(p => p.Category).FirstOrDefault(p => p.ID == id);
            if (product == null)
            {
                return NotFound();
            }
            // Список категорий для выбора
            ViewBag.Categories = _context._categories.Select(c => new SelectListItem
            {
                Value = c.ID.ToString(),
                Text = c.Name
            }).ToList();
            return View(product);
        }

        // Обработка формы редактирования товара
        [HttpPost]
        public async Task<IActionResult> EditProduct(int id, Product product)
        {
            if (ModelState.IsValid)
            {
                _context._products.Attach(product);
                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Товар успешно обновлен.";
                return RedirectToAction("Products");
            }
            // Список категорий для выбора
            ViewBag.Categories = _context._categories.Select(c => new SelectListItem
            {
                Value = c.ID.ToString(),
                Text = c.Name
            }).ToList();
            return View(product);
        }

        // Обработка удаления товара
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context._products.FindAsync(id);
            if (product != null)
            {
                _context._products.Remove(product);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Товар успешно удален.";
            }
            return RedirectToAction("Products");
        }
    }
}
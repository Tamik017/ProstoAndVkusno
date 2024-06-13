using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ProstoAndVkusno.Data.Models;
using ProstoAndVkusno.DBContext;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProstoAndVkusno.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        ApplicationContext _context;
        public AdminController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Admin()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Users()
        {
            var users = _context._users.ToList();
            return View(users);
        }

        [HttpGet]
        public IActionResult Products()
        {
            var products = _context._products.Include(p => p.Category).ToList();
            return View(products);
        }

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

        [HttpPost]
        public async Task<IActionResult> EditUser(int id, Users model)
        {
            ModelState.Remove("Password"); // Не обновляем пароль, если он не был изменен
            if (ModelState.IsValid)
            {
                var user = await _context._users.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                // Обновляем только те поля, которые были изменены
                user.Login = model.Login;
                user.Email = model.Email;
                user.Role = model.Role;

                _context._users.Update(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Users");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult GetProduct(int productId)
        {
            var product = _context._products.Include(p => p.Category).FirstOrDefault(p => p.ID == productId);

            if (product == null)
            {
                return NotFound();
            }

            // Используйте System.Text.Json для сериализации
            return Json(product, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            // Загрузите список категорий для селекта
            ViewBag.Categories = _context._categories.Select(c => new SelectListItem
            {
                Value = c.ID.ToString(),
                Text = c.Name
            }).ToList();

            return View(new Product());
        }

        // Метод для обработки создания товара
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            bool temp = true;
            if (temp)
            //if (ModelState.IsValid)
            {
                _context._products.Add(product);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Товар успешно добавлен.";
                return RedirectToAction("Products");
            }

            // Загрузите список категорий для селекта
            ViewBag.Categories = _context._categories.Select(c => new SelectListItem
            {
                Value = c.ID.ToString(),
                Text = c.Name
            }).ToList();

            return View(product);
        }

        // Метод для отображения страницы редактирования товара
        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            var product = _context._products.Include(p => p.Category).FirstOrDefault(p => p.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            // Загрузите список категорий для селекта
            ViewBag.Categories = _context._categories.Select(c => new SelectListItem
            {
                Value = c.ID.ToString(),
                Text = c.Name
            }).ToList();

            return View(product);
        }

        // Метод для обработки редактирования товара
        [HttpPost]
        public async Task<IActionResult> EditProduct(int id, Product product)
        {
            //bool temp = true;
            //if (temp)
            if(ModelState.IsValid)
            {
                _context._products.Attach(product);
                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Товар успешно обновлен.";
                return RedirectToAction("Products");
            }

            // Список категорий для селекта
            ViewBag.Categories = _context._categories.Select(c => new SelectListItem
            {
                Value = c.ID.ToString(),
                Text = c.Name
            }).ToList();

            return View(product);
        }

        // Метод для обработки удаления товара
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

        [HttpGet]
        public IActionResult AddProduct()
        {
            // Загружаем список категорий
            ViewBag.Categories = _context._categories.Select(c => new SelectListItem
            {
                Value = c.ID.ToString(),
                Text = c.Name
            }).ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product model)
        {
            if (ModelState.IsValid)
            {
                _context._products.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Products");
            }
            ViewBag.Categories = _context._categories.Select(c => new SelectListItem
            {
                Value = c.ID.ToString(),
                Text = c.Name
            }).ToList();
            return View(model);
        }
    }
}
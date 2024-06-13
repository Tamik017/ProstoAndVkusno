using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProstoAndVkusno.DBContext;
using ProstoAndVkusno.Data.Models;
using System.Security.Claims;
using ProstoAndVkusno.Filters;
using Microsoft.EntityFrameworkCore;

namespace ProstoAndVkusno.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationContext _context;

        // Инициализация контекста базы данных
        public LoginController(ApplicationContext context)
        {
            _context = context;
        }

        // Страница регистрации (доступна только для незарегистрированных пользователей)
        [HttpGet]
        [RedirectAuthenticatedUser] // Фильтр для перенаправления авторизованных пользователей
        public IActionResult Registration()
        {
            ViewBag.Title = "Регистрация";
            return View();
        }

        // Обработка формы регистрации
        [HttpPost]
        public async Task<IActionResult> Registration(Registration model)
        {
            if (ModelState.IsValid)
            {
                // Проверка, существует ли пользователь с таким логином или email
                var existingUser = _context._users.FirstOrDefault(u => u.Login == model.Login || u.Email == model.Email);
                if (existingUser == null)
                {
                    // Создание нового пользователя
                    var user = new Users
                    {
                        Login = model.Login,
                        Email = model.Email,
                        Password = model.Password,
                        Role = "user"
                    };

                    _context._users.Add(user);
                    await _context.SaveChangesAsync();

                    // Создание токена аутентификации
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Login),
                        new Claim(ClaimTypes.Role, user.Role)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    // Аутентификация пользователя
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    // Перенаправление на страницу профиля
                    return RedirectToAction("Profile", "Login");
                }

                // Ошибка: Пользователь с таким логином или email уже существует
                ModelState.AddModelError("", "Пользователь с таким логином или email уже существует");
            }

            return View(model);
        }

        // Страница входа (доступна только для незарегистрированных пользователей)
        [HttpGet]
        [RedirectAuthenticatedUser]
        public IActionResult Login()
        {
            ViewBag.Title = "Вход";
            return View();
        }

        // Обработка формы входа
        [HttpPost]
        public async Task<IActionResult> Login(Login model)
        {
            if (ModelState.IsValid)
            {
                // Поиск пользователя по логину и паролю
                var user = _context._users.FirstOrDefault(u => u.Login == model.Username && u.Password == model.Password);
                if (user != null)
                {
                    // Создание токена аутентификации
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Login),
                        new Claim(ClaimTypes.Role, user.Role)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    // Аутентификация пользователя
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    // Перенаправление на соответствующую страницу в зависимости от роли
                    if (user.Role == "user")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (user.Role == "admin")
                    {
                        return RedirectToAction("Profile", "Login");
                    }
                }

                // Ошибка: Неверный логин или пароль
                ModelState.AddModelError("", "Неверный логин или пароль");
            }

            return View(model);
        }

        // Страница профиля (доступна только для авторизованных пользователей)
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            // Получение логина пользователя из токена аутентификации
            var userLogin = User.Identity.Name;
            var user = await _context._users.FirstOrDefaultAsync(u => u.Login == userLogin);

            if (user == null)
            {
                return RedirectToAction("Login");
            }

            // Создание модели представления профиля
            var model = new UserProfile
            {
                ID = user.ID,
                Login = user.Login,
                Email = user.Email
            };

            ViewBag.Title = "Профиль";

            return View(model);
        }

        // Обработка формы обновления профиля
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Profile(UserProfile model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context._users.FirstOrDefaultAsync(u => u.ID == model.ID);

                if (user != null)
                {
                    // Проверка старого пароля
                    if (!string.IsNullOrEmpty(model.OldPassword) && model.OldPassword == user.Password)
                    {
                        // Проверка нового пароля
                        if (!string.IsNullOrEmpty(model.Password) && model.Password == model.ConfirmPassword)
                        {
                            // Обновление пароля
                            user.Password = model.Password;
                        }
                        // Обновление email
                        user.Email = model.Email;
                        _context._users.Update(user);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Profile");
                    }
                    // Ошибка: Неверный старый пароль или несовпадение новых паролей
                    ModelState.AddModelError("", "Старый пароль неверен или новые пароли не совпадают");
                }
            }

            return View(model);
        }

        // Выход из системы
        public async Task<IActionResult> Logout()
        {
            // Удаление токена аутентификации
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }
    }
}
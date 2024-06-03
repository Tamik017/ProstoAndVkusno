using Microsoft.AspNetCore.Mvc;

namespace ProstoAndVkusno.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string studentName, string groupName)
        {
            return View(new List<string>() { studentName, groupName });
        }
    }
}

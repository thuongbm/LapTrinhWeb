using Microsoft.AspNetCore.Mvc;

namespace Lab1.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

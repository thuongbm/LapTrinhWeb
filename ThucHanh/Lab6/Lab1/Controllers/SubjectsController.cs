using Microsoft.AspNetCore.Mvc;

namespace Lab1.Controllers
{
    public class SubjectsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

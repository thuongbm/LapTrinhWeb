using Microsoft.AspNetCore.Mvc;

namespace Study1.Controllers
{
    public class MyController : Controller
    {
        public IActionResult Index()
        {
            //ViewData["Name"] = "MyController";
            return View();
        }
    }
}

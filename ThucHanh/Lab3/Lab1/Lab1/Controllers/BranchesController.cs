using Microsoft.AspNetCore.Mvc;

namespace Lab1.Controllers
{
    public class BranchesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

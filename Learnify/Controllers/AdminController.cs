using Microsoft.AspNetCore.Mvc;

namespace Learnify.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

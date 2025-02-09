using Learnify.Models;
using Microsoft.AspNetCore.Mvc;

namespace Learnify.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(loginDetails login)
        {
            if (ModelState.IsValid)
            {
                if (login.email =="admin"&& login.password == "admin")
                {
                    return RedirectToAction("dashboard", "Admin");
                }
                else
                {
                    ViewBag.error = "Invalid Credentials";
                    return View();
                }
            }
            return View();
        }

        public IActionResult dashboard()
        {
            return View();
        }

    }
}

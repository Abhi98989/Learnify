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
<<<<<<< HEAD
        public IActionResult Index(loginDetails login)
        {
            if (ModelState.IsValid)
            {
                if (login.email =="admin"&& login.password == "admin")
=======
        public IActionResult Index(loginDetails loginDetails)
        {
            if (ModelState.IsValid)
            {
                if (loginDetails.email =="admin"&& loginDetails.password == "admin")
>>>>>>> 636a531ad454d211736c2951fb280396480bf2ec
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

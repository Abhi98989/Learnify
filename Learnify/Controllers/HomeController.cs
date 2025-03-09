using Learnify.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using Learnify.Classes;
using Microsoft.AspNetCore.Http;

namespace Learnify.Controllers
{
    public class HomeController : Controller
    {
        DataHandeler dh = new DataHandeler();
        public IActionResult Index()
        {
            ViewBag.Message = HttpContext.Session.GetString("isLoggedIn");
            ViewBag.isAdmin = HttpContext.Session.GetString("isAdmin");
			ViewBag.User = HttpContext.Session.GetString("fullname");
            return View();
        }
        [HttpPost]
		public IActionResult LogOut()
		{
            HttpContext.Session.Clear();
			return RedirectToAction("index", "Home");
		}

		public IActionResult login()
        {
			ViewBag.Message = HttpContext.Session.GetString("isLoggedIn");
			ViewBag.isAdmin = HttpContext.Session.GetString("isAdmin");
			ViewBag.User = HttpContext.Session.GetString("fullname");
			return View();
        }

        public IActionResult about()
        {
			ViewBag.Message = HttpContext.Session.GetString("isLoggedIn");
            ViewBag.isAdmin = HttpContext.Session.GetString("isAdmin");
			ViewBag.User = HttpContext.Session.GetString("fullname");
			return View();
        }
		public IActionResult Teams()
		{
			ViewBag.Message = HttpContext.Session.GetString("isLoggedIn");
            ViewBag.isAdmin = HttpContext.Session.GetString("isAdmin");
			ViewBag.User = HttpContext.Session.GetString("fullname");
			return View();
		}
		public IActionResult testimonials()
		{
			ViewBag.Message = HttpContext.Session.GetString("isLoggedIn");
            ViewBag.isAdmin = HttpContext.Session.GetString("isAdmin");
			ViewBag.User = HttpContext.Session.GetString("fullname");
			return View();
		}
		public IActionResult Services()
		{
			ViewBag.Message = HttpContext.Session.GetString("isLoggedIn");
            ViewBag.isAdmin = HttpContext.Session.GetString("isAdmin");
			ViewBag.User = HttpContext.Session.GetString("fullname");
			return View();
		}
		public IActionResult Ourcourses()
        {
			ViewBag.Message = HttpContext.Session.GetString("isLoggedIn");
            ViewBag.isAdmin = HttpContext.Session.GetString("isAdmin");
			ViewBag.User = HttpContext.Session.GetString("fullname");
			return View();
        }
		public IActionResult CourseDetails()
		{
			ViewBag.Message = HttpContext.Session.GetString("isLoggedIn");
            ViewBag.isAdmin = HttpContext.Session.GetString("isAdmin");
			ViewBag.User = HttpContext.Session.GetString("fullname");
			return View();
		}
		public IActionResult register()
        {
			ViewBag.Message = HttpContext.Session.GetString("isLoggedIn");
            ViewBag.isAdmin = HttpContext.Session.GetString("isAdmin");
			ViewBag.User = HttpContext.Session.GetString("fullname");
			return View();
        }
        public IActionResult Pricing()
        {
			ViewBag.Message = HttpContext.Session.GetString("isLoggedIn");
            ViewBag.isAdmin = HttpContext.Session.GetString("isAdmin");
			ViewBag.User = HttpContext.Session.GetString("fullname");
			return View();
        }
        public IActionResult Contact()
        {
			ViewBag.Message = HttpContext.Session.GetString("isLoggedIn");
            ViewBag.isAdmin = HttpContext.Session.GetString("isAdmin");
			ViewBag.User = HttpContext.Session.GetString("fullname");
			return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //------------------Data handling logic paxi xuutai xuttai controller rw aarko DataAcessLayer banayera halne------------------------------

        [HttpPost]
        public IActionResult register(register r1)
        {

			try
			{
				if (r1.password != r1.confirmpassword)
				{
					ViewBag.Message = "Password and Confirm Password should be same";
					return View();
				}
				SqlParameter[] param =
				{
					new SqlParameter("@FullName", r1.FullName),
					new SqlParameter("@email", r1.email),
					new SqlParameter("@password", r1.password),
				};
				int res = dh.InsertUpdate("User_register", param, CommandType.StoredProcedure);
				if (res > 0)
				{
					ViewBag.Message = "Data Inserted Successfully";
					return RedirectToAction("index", "Home");
				}
				else
				{
					ViewBag.Message = "Data Insertion Failed";
                    return View();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}


        [HttpPost]
        public IActionResult Login(loginDetails lg)
        {
			try
			{
				SqlParameter[] param =
                {
					new SqlParameter("@email", lg.email),
					new SqlParameter("@password", lg.password),
				};
				DataTable res = dh.ReadData("User_Login", param, CommandType.StoredProcedure);
                if(res.Rows.Count > 0)
                {
                    ViewBag.Message = res.Rows[0]["FullName"].ToString();
                    HttpContext.Session.SetString("email", res.Rows[0]["email"].ToString());
                    HttpContext.Session.SetString("fullname", res.Rows[0]["FullName"].ToString());
                    HttpContext.Session.SetString("isAdmin", res.Rows[0]["isAdmin"].ToString());
					HttpContext.Session.SetString("isLoggedIn", "1");
					return RedirectToAction("index", "Home");
				}
                else
                {
					ViewBag.Message = "Invalid Credentials";
                    return View();
				}
			}
			catch (Exception ex)
			{
                throw ex;
			}
        }
    }
}

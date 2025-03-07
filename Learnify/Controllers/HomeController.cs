using Learnify.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace Learnify.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly String Dbcs;

        public HomeController(IConfiguration configuration)
        {
            Dbcs = configuration.GetConnectionString("DefaultConnection");
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult login()
        {
            return View();
        }

        public IActionResult about()
        {
            return View();
        }

        public IActionResult Ourcourses()
        {
            return View();
        }
        public IActionResult register()
        {
            return View();
        }
        public IActionResult Pricing()
        {
            return View();
        }
        public IActionResult Contact()
        {
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

            if (r1.password != r1.confirmpassword)
            {
                ViewBag.Message = "Password and Confirm Password should be same";
                return View();
            }
            try
            {
                using SqlConnection con = new SqlConnection(Dbcs);
                {
                    using SqlCommand cmd = new SqlCommand("User_register", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FullName", r1.FullName);
                    cmd.Parameters.AddWithValue("@email", r1.email);
                    cmd.Parameters.AddWithValue("@password", r1.password);
                    con.Open();
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        ViewBag.Message = "Data Inserted Successfully";
                        return View("index");
                    }
                    else
                    {
                        ViewBag.Message = "Data Inserted Failed";
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }


            return View();
        }


        [HttpPost]
        public IActionResult Login(loginDetails lg)

        {
            if (lg == null)
            {
                return View();
            }
            try
            {
                using SqlConnection con = new SqlConnection(Dbcs);
                {
                    using SqlCommand cmd = new SqlCommand("[User_Login]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", lg.email);
                    cmd.Parameters.AddWithValue("@password", lg.password);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        return RedirectToAction("about", "Home");
                    }
                    else
                    {
                        ViewBag.Message = "Invalid Credentials";
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }



            return View();
        }
    }
}

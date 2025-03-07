
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Learnify.Models;
using System.Data.SqlClient;
using Learnify.Classes;

namespace Learnify.Controllers
{
	public class AdminController : Controller
	{
		DataHandeler dh = new DataHandeler();
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult Book()
		{
			return View();
		}
		public IActionResult Category()
		{
			return View();
		}
		public IActionResult GetCategory()
		{
			try
			{
				string res = dh.ReadToJson("Usp_S_Category", null, CommandType.StoredProcedure);
				JArray jArray = (JArray)JsonConvert.DeserializeObject(res);
				return Content(res, "application/json"); ;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IActionResult InsertUpdateCategory([FromBody] Category cat)
		{
			try
			{
				SqlParameter[] param =
				{
					new SqlParameter("@CategoryId", cat.CategoryId),
					new SqlParameter("@CategoryName", cat.CategoryName),
				};
				int res = dh.InsertUpdate("Usp_IU_Category", param, CommandType.StoredProcedure);
				return Json(res);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IActionResult TrashCategory([FromBody] Category cat)
		{
			try
			{
				SqlParameter[] param =
				{
					new SqlParameter("@CategoryId", cat.CategoryId),
				};
				int res = dh.InsertUpdate("Usp_D_Category", param, CommandType.StoredProcedure);
				return Json(res);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public IActionResult GetBooks()
		{
			try
			{
				string res = dh.ReadToJson("[Usp_S_Books]", null, CommandType.StoredProcedure);
				JArray jArray = (JArray)JsonConvert.DeserializeObject(res);
				return Content(res, "application/json"); ;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IActionResult InsertUpdateBook([FromBody] Books book)
		{
			try
			{
				SqlParameter[] param =
				{
					new SqlParameter("@BookId", book.BookId),
					new SqlParameter("@CategoryId", book.Category),
					new SqlParameter("@BookName", book.BookName),
				};
				int res = dh.InsertUpdate("[Usp_IU_Book]", param, CommandType.StoredProcedure);
				return Json(res);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IActionResult TrashBook([FromBody] Books book)
		{
			try
			{
				SqlParameter[] param =
				{
					new SqlParameter("@BookId", book.BookId),
				};
				int res = dh.InsertUpdate("Usp_D_Book", param, CommandType.StoredProcedure);
				return Json(res);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}

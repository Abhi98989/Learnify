
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Learnify.Models;
using System.Data.SqlClient;
using Learnify.Classes;
using Learnify.DataAccess;

namespace Learnify.Controllers
{
	public class AdminController : Controller
	{
		DataHandeler dh = new DataHandeler();
		public IActionResult Index()
		{
			string? isAdmin = HttpContext.Session.GetString("isAdmin");

			if (isAdmin?.ToLower() == "true")
			{
				return View();
			}
			else
			{
				return RedirectToAction("index", "Home");
			}
		}
		public IActionResult Book()
		{
			string? isAdmin = HttpContext.Session.GetString("isAdmin");

			if (isAdmin?.ToLower() == "true")
			{
				return View();
			}
			else
			{
				return RedirectToAction("index", "Home");
			}
		}
		public IActionResult Category()
		{
			string? isAdmin = HttpContext.Session.GetString("isAdmin");

			if (isAdmin?.ToLower() == "true")
			{
				return View();
			}
			else
			{
				return RedirectToAction("index", "Home");
			}
		}
		public IActionResult GetCategory()
		{
			try
			{
				string res = dh.ReadToJson("Usp_S_Category", null, CommandType.StoredProcedure);
				return Content(res, "application/json");
			}
			catch (Exception ex)
			{
				throw;
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
				throw;
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
				throw;
			}
		}

		public IActionResult GetBooks()
		{
			try
			{
				string res = dh.ReadToJson("[Usp_S_Books]", null, CommandType.StoredProcedure);
				return Content(res, "application/json"); ;
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		public IActionResult GetBookById([FromBody] Books bk)
		{
			try
			{
				SqlParameter[] param =
				{
					new SqlParameter("@BookId", bk.BookId),
				};
				string res = dh.ReadToJson("[Usp_S_BookById]", param, CommandType.StoredProcedure);
				return Content(res, "application/json"); ;
			}
			catch (Exception ex)
			{
				throw;
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
					new SqlParameter("@AuthorName", book.AuthorName),
					new SqlParameter("@PublishedOn", book.PublishedOn),
					new SqlParameter("@Description", book.Description),
					new SqlParameter("@FileName", book.FileName),
				};
				int res = dh.InsertUpdate("[Usp_IU_Book]", param, CommandType.StoredProcedure);
				return Json(res);
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		[HttpPost]
		public async Task<IActionResult> UploadFile(IFormFile file)
		{
			if (file == null || file.Length == 0)
			{
				return BadRequest("No file selected.");
			}

			try
			{
				string? uploadedFile = await FileProcessing.UploadFileAsync(file, "Books");
				return Json(uploadedFile);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal Server Error: {ex.Message}");
			}
		}
		[HttpPost]
		public async Task<IActionResult> DeleteFile([FromBody] Books book)
		{
			bool isDeleted = await FileProcessing.DeleteFileAsync(book.FileName, "Books");

			if (isDeleted)
			{
				return Json("200");
			}
			return NotFound("404");
		}

		public async Task<IActionResult> TrashBookAsync([FromBody] Books book)
		{
			try
			{
				int res = 0;
				bool isDeleted = true;
				if (book.FileName != "")
				{
					isDeleted = await FileProcessing.DeleteFileAsync(book.FileName, "Books");
				}
				if (isDeleted)
				{
					SqlParameter[] param =
					{
					new SqlParameter("@BookId", book.BookId),
				};
					res = dh.InsertUpdate("Usp_D_Book", param, CommandType.StoredProcedure);
				}
				return Json(res);
			}
			catch (Exception ex)
			{
				throw;
			}
		}
	}
}

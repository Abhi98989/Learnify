
using System.Security.Cryptography;
using System.Text;

namespace Learnify.DataAccess
{
	public class FileProcessing
	{
		public static async Task<string> UploadFileAsync(IFormFile file, string folder)
		{
			try
			{
				string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/" + folder);
				if (!Directory.Exists(uploadsFolder))
				{
					Directory.CreateDirectory(uploadsFolder);
				}
				string fileExtension = Path.GetExtension(file.FileName);

				string hashedFileName = GenerateHashedFileName(file.FileName) + fileExtension;

				string filePath = Path.Combine(uploadsFolder, hashedFileName);

				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					await file.CopyToAsync(stream);
				}
				return hashedFileName;
			}
			catch (Exception ex)
			{
				return ex.ToString();
			}
		}
		public static Task<bool> DeleteFileAsync(string fileName, string folder)
		{
			try
			{
				string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/" + folder);

				string filePath = Path.Combine(uploadsFolder, fileName);

				if (File.Exists(filePath))
				{
					File.Delete(filePath);
					return Task.FromResult(true);
				}
				return Task.FromResult(false); 
			}
			catch (Exception)
			{
				return Task.FromResult(false);
			}
		}

		private static string GenerateHashedFileName(string fileName)
		{
			using (SHA256 sha256 = SHA256.Create())
			{
				byte[] inputBytes = Encoding.UTF8.GetBytes(fileName + Guid.NewGuid().ToString());
				byte[] hashBytes = sha256.ComputeHash(inputBytes);

				// Convert to Base64 and take first 10 characters for short name
				return BitConverter.ToString(hashBytes).Replace("-", "").Substring(0, 10);
			}
		}
	}
}

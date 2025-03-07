namespace Learnify.Models
{
	public class Books
	{
		public int BookId { get; set; }
		public string? BookName { get; set; }
		public int Category { get; set; }
	}
	public class Category
	{
		public int CategoryId { get; set; }
		public string? CategoryName { get; set; }
	}
}

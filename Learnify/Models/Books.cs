namespace Learnify.Models
{
	public class Books
	{
		public int BookId { get; set; }
		public string? BookName { get; set; }
		public int Category { get; set; }
		public string? AuthorName{ get; set; }
		public string? PublishedOn{ get; set; }
		public string? Description{ get; set; }
		public string? FileName{ get; set; }
	}
	public class Category
	{
		public int CategoryId { get; set; }
		public string? CategoryName { get; set; }
	}
}

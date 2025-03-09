using System.ComponentModel.DataAnnotations;

namespace Learnify.Models
{
    public class loginDetails
    {
        public string? email{ get; set; }
        public string? password { get; set; }
        public bool isAdmin { get; set; }
	}
}

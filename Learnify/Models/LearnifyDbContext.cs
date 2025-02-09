using Microsoft.EntityFrameworkCore;

namespace Learnify.Models
{
    public class LearnifyDbContext : DbContext
    {
        public LearnifyDbContext(DbContextOptions<LearnifyDbContext> options)
            : base(options)
        {
        }

        // Add DbSet properties for each entity here
        public DbSet<login> LoginDetails { get; set; }
    }
}

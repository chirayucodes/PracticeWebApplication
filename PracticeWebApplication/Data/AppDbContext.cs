using Microsoft.EntityFrameworkCore;
namespace PracticeWebApplication.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<StudentDetails> StudentDetails { get; set; }
    }
}
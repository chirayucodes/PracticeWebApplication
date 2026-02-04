using Microsoft.EntityFrameworkCore;
namespace PracticeWebApplication.Data
{
    public sealed class AppDbContext : DbContext
    {
        public DbSet<StudentDetails> StudentDetails { get; init; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "server=DESKTOP-1GU3G65;database=testdbb;TrustServerCertificate=true;Trusted_Connection=true;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}


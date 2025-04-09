using Microsoft.EntityFrameworkCore;
using backend.Controllers; // ✅ This is where your Report class lives


namespace backend
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Report> Reports { get; set; }
    }
}

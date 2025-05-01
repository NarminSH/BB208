using BB208MVCIntro.Models;
using Microsoft.EntityFrameworkCore;

namespace BB208MVCIntro.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions): base(dbContextOptions) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=DELLNARMIN;Database=Bb208PurpleBuzz;TrustServerCertificate=True;Trusted_Connection=True");
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Service> Services { get; set; }
    }
}

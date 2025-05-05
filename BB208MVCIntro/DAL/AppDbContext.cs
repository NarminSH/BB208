using BB208MVCIntro.Models;
using Microsoft.EntityFrameworkCore;

namespace BB208MVCIntro.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions): base(dbContextOptions) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Service> Services { get; set; }
    }
}

using BB208MVCIntro.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Emit;

namespace BB208MVCIntro.DAL
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions): base(dbContextOptions) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Service> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Category>().Property(c => c.Title).HasMaxLength(20);

           

            builder.Entity<Service>().Property(s=>s.Title).HasMaxLength(20);
            builder.Entity<Service>().Property(s=>s.Description).HasMaxLength(100);
            builder.Entity<Service>().HasOne(s => s.Category).WithMany(s => s.Services)
                .HasForeignKey(s => s.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            

        }
    }
}

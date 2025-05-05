using BB208MVCIntro.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace BB208MVCIntro
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer("Server=DELLNARMIN;Database=Bb208PurpleBuzz;TrustServerCertificate=True;Trusted_Connection=True");
            }); 
            var app = builder.Build();
            app.UseStaticFiles();
            app.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
                );
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            

            

            app.Run();
        }
    }
}

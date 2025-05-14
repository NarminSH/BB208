using BB208MVCIntro.DAL;
using BB208MVCIntro.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BB208MVCIntro
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("MsSql"));
            });
            builder.Services.AddIdentity<AppUser,IdentityRole>
                (opt =>
                {
                    opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_@";
                    opt.User.RequireUniqueEmail = true;
                    opt.Password.RequiredLength = 8;
                    opt.SignIn.RequireConfirmedEmail = true;
                    opt.Lockout.MaxFailedAccessAttempts = 7;
                    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);

                }).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();
            var app = builder.Build();

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
           
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

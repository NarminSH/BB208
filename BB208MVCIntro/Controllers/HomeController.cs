
using BB208MVCIntro.DAL;
using BB208MVCIntro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace BB208MVCIntro.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db; //depe
        public HomeController(AppDbContext context)
        {
            _db= context;
        }
        public IActionResult Index()
        {
            List<Service> services;

            services = _db.Services.Include(s => s.Category).ToList();
            ViewBag.Categories = _db.Categories.ToList();

            return View(services);
        }
        public IActionResult About()
        {
             
            return View();
        }
    }
}

using BB208MVCIntro.DAL;
using BB208MVCIntro.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BB208MVCIntro.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _db;
        public DashboardController(AppDbContext appDbContext)
        {
            _db = appDbContext;
        }
        public IActionResult Index()
        {
            var categories = _db.Categories.Include(c=>c.Services).ToList();
            return View(categories);
        }
      

    }
}

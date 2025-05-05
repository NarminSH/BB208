using BB208MVCIntro.DAL;
using BB208MVCIntro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BB208MVCIntro.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _db;
        public CategoryController(AppDbContext appDbContext)
        {
            _db = appDbContext;
        }
        public IActionResult Index()
        {
            var categories = _db.Categories.Include(c => c.Services).ToList();
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Something went wrong");
            }
            _db.Categories.Add(category);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            Category? category = _db.Categories.Find(id);
            if (category == null) { return NotFound("Category could not be found"); }
            _db.Categories.Remove(category);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
           Category? existingCategory= _db.Categories.Find(id);
            if (existingCategory == null) { return NotFound("Category could not be found"); }
            return View(nameof(Create), existingCategory);
        }
        [HttpPost]
        public IActionResult Update(Category category)
        {
            if (!ModelState.IsValid) 
            {
                return View(nameof(Create), category);
            }
            Category? existingCategory = _db.Categories.AsNoTracking()
                .FirstOrDefault(c => c.Id == category.Id);
            if (existingCategory == null) { return NotFound("Category could not be found"); }

            _db.Categories.Update(category);
            //existingCategory.Title = category.Title;
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}

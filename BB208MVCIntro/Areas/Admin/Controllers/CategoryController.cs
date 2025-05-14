using BB208MVCIntro.Areas.ViewModels.CategoryVMs;
using BB208MVCIntro.DAL;
using BB208MVCIntro.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BB208MVCIntro.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
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
            Service? services = _db.Services.FirstOrDefault(s => s.CategoryId == category.Id);
            if (services != null)
            {
                return BadRequest("Database-da bu categoryId-li service var. Sen bu category-ni sile bilmersen!");
            }
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

        public IActionResult AssignServices(int id)
        {
            Category? existingCategory = _db.Categories.Find(id);
            if (existingCategory == null) { return NotFound("Category could not be found"); }
            BatchAssignVM batchAssignVM = new BatchAssignVM()
            {
                CategoryId = id,
                AllServices = _db.Services.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Title }).ToList(),
                ServiceIds = new List<int> ()
            };
            return View(batchAssignVM);
        }

        [HttpPost]
        public IActionResult AssignServices(BatchAssignVM model)
        {
            if ( model.ServiceIds == null || model.ServiceIds.Any() )
            {
                ModelState.AddModelError("", "Add at least one service to category");
                BatchAssignVM batchAssignVM = new BatchAssignVM()
                {
                    CategoryId = model.CategoryId,
                    AllServices = _db.Services.Select(s =>
                    new SelectListItem { Value = s.Id.ToString(), Text = s.Title }).ToList(),
                    ServiceIds = new List<int>()
                };
                return View(batchAssignVM);
            }

            var services = _db.Services.Where(s => model.ServiceIds.Contains(s.Id));
            foreach (var service in services)
            {
                service.CategoryId = model.CategoryId;
            };
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}

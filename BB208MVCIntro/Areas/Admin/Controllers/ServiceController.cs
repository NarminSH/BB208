using BB208MVCIntro.Areas.ViewModels.ServiceVMs;
using BB208MVCIntro.DAL;
using BB208MVCIntro.Models;
using BB208MVCIntro.Utilities.File;
using BB208MVCIntro.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace BB208MVCIntro.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Manager")]
    public class ServiceController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _webEnvironment;
        public ServiceController(AppDbContext appDbContext, IWebHostEnvironment webEnvironment)
        {
            _db = appDbContext;
            _webEnvironment = webEnvironment;
        }
        public IActionResult Index()
        {
            var services = _db.Services.Include(c => c.Category).ToList();
            return View(services);
        }
        public IActionResult Create()
        {

            ViewBag.AllCategories = _db.Categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Title }).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateServiceVM createServiceVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.AllCategories = _db.Categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Title }).ToList();
                return View(createServiceVM);
            }

            Category? existingCategory = _db.Categories.Find(createServiceVM.CategoryId);
            if (existingCategory == null)
            {
                ModelState.AddModelError("CategoryId", "Provided category doesn't exist");
                return View(createServiceVM);
            }

            if (!createServiceVM.ImageUpload.CheckImageType())
            {
                ModelState.AddModelError("ImageUpload", "File must be Image format");
                return View(createServiceVM);
            }
            string filename = createServiceVM.ImageUpload.DownloadImage(_webEnvironment, @"\UploadImages\Services\");
            Service service = new Service()
            {
                ImgUrl = filename,
                Title = createServiceVM.Title,
                Description = createServiceVM.Description,
                CategoryId = createServiceVM.CategoryId
            };
            _db.Services.Add(service);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            Service existingService = _db.Services.Find(id);
            if (existingService == null) { return NotFound("Service could not be found"); }
            UpdateServiceVM updateServiceVM = new UpdateServiceVM()
            {
                Id = id,
                Title = existingService.Title,
                Description = existingService.Description,
                ExistingImageUrl = existingService.ImgUrl
            };
            ViewBag.AllCategories = new SelectList(_db.Categories.ToList(), "Id", "Title", selectedValue:existingService.CategoryId);
            return View(updateServiceVM);
        }

        [HttpPost]
        public IActionResult Update(UpdateServiceVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Service? service=_db.Services.Find(model.Id);
            if (service == null) { return NotFound("Service couldn't be found"); }
            service.Title = model.Title;
            service.Description = model.Description;
            service.CategoryId = model.CategoryId;
            if (model.ImageUpload != null) 
            {
                string existingImagePath = _webEnvironment.WebRootPath + @"\UploadImages\Services\" + model.ExistingImageUrl;
                if (System.IO.File.Exists(existingImagePath))
                {
                    System.IO.File.Delete(existingImagePath);
                }
                if (model.ImageUpload.CheckImageType())
                {
                    string newfileName = model.ImageUpload.DownloadImage(_webEnvironment, @"\UploadImages\Services\");
                    service.ImgUrl = newfileName;
                }
            }
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));


        }

        public IActionResult Delete(int id)
        {
            Service? service = _db.Services.Find(id);
            if (service == null) { return NotFound("Service could not be found"); }
            string imagePath = _webEnvironment.WebRootPath + @"\UploadImages\Services\" + service.ImgUrl;
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            _db.Services.Remove(service);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));


        }
       

    }
}
